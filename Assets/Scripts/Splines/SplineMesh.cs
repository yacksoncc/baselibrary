﻿using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace BezierCurve
{
   [RequireComponent(typeof(CinemachineSmoothPath))]
   public class SplineMesh : MonoBehaviour
   {
      [SerializeField]
      private CinemachineSmoothPath refCinemachineSmoothPath;

      [SerializeField]
      private Mesh meshToFillSpline;

      [SerializeField]
      private Material materialSpline;

      [SerializeField]
      private int quantitySubdividedMeshs = 1;

      private GameObject[] arrayGameObjectsSegmentsMeshFilters;

      private SamplePositionAndRotation[] arraySamplePosition = Array.Empty<SamplePositionAndRotation>();

      [Header("Debug")]
      [SerializeField]
      private bool debug;

      private void Awake()
      {
         FillSamplePositions();
         CreateSubdividedMesh();

         int tmpIndexSegmentMeshFilter = 0;
         int tmpIndexLenghTraveledThrougSpline = 0;
         var tmpSegmentSplineSize = Mathf.CeilToInt(((float)arraySamplePosition.Length) / quantitySubdividedMeshs);

         foreach(var tmpGameObjectsSegmentsMeshFilter in arrayGameObjectsSegmentsMeshFilters)
         {
            var tmpListVertices = new List<Vector3>();
            var tmpListTriangles = new List<int>();
            var tmpListNormals = new List<Vector3>();
            var tmpListUVs = new List<Vector2>();
            var tmpIndexTriangles = 0;

            var tmpMeshRender = tmpGameObjectsSegmentsMeshFilter.GetComponent<MeshFilter>();
            var tmpBoundsCenter = arraySamplePosition[tmpSegmentSplineSize * tmpIndexSegmentMeshFilter].position;
            var tmpBounds = new Bounds(tmpBoundsCenter, Vector3.zero); //center by defaul is take in account

            for(; tmpIndexLenghTraveledThrougSpline < arraySamplePosition.Length - 1; tmpIndexLenghTraveledThrougSpline++)
            {
               for(int tmpIndexVertexMeshToCopy = 0; tmpIndexVertexMeshToCopy < meshToFillSpline.vertices.Length; tmpIndexVertexMeshToCopy++)
               {
                  var tmpVertexPosition = meshToFillSpline.vertices[tmpIndexVertexMeshToCopy];

                  Vector3 tmpPositionOnPath;
                  Quaternion tmpRotationVertex;

                  if(Math.Abs(tmpVertexPosition[2]) < 0.005f)
                  {
                     tmpPositionOnPath = arraySamplePosition[tmpIndexLenghTraveledThrougSpline].position;
                     tmpRotationVertex = arraySamplePosition[tmpIndexLenghTraveledThrougSpline].rotation;
                  }
                  else
                  {
                     tmpPositionOnPath = arraySamplePosition[tmpIndexLenghTraveledThrougSpline + 1].position;
                     tmpRotationVertex = arraySamplePosition[tmpIndexLenghTraveledThrougSpline + 1].rotation;
                     tmpVertexPosition[2] = 0;
                  }

                  var tmpVertexPositionForNewMesh = tmpPositionOnPath + tmpRotationVertex * tmpVertexPosition;
                  tmpBounds.Encapsulate(tmpVertexPositionForNewMesh);
                  tmpListVertices.Add(tmpVertexPositionForNewMesh);
                  tmpListNormals.Add(tmpRotationVertex * meshToFillSpline.normals[tmpIndexVertexMeshToCopy]);
                  tmpListUVs.Add(meshToFillSpline.uv[tmpIndexVertexMeshToCopy]);
               }

               foreach(var tmpTriangleIndex in meshToFillSpline.triangles)
                  tmpListTriangles.Add(tmpTriangleIndex + tmpIndexTriangles);

               tmpIndexTriangles += meshToFillSpline.vertices.Length;

               if(Mathf.FloorToInt((float)tmpIndexLenghTraveledThrougSpline / tmpSegmentSplineSize) > tmpIndexSegmentMeshFilter)
               {
                  tmpIndexSegmentMeshFilter++;
                  tmpIndexLenghTraveledThrougSpline++;
                  break;
               }
            }

            tmpMeshRender.transform.position = tmpBounds.center;

            for(int i = 0; i < tmpListVertices.Count; i++)
               tmpListVertices[i] = tmpMeshRender.transform.InverseTransformPoint(tmpListVertices[i]);

            tmpMeshRender.mesh.SetVertices(tmpListVertices);
            tmpMeshRender.mesh.SetTriangles(tmpListTriangles, 0);
            tmpMeshRender.mesh.SetNormals(tmpListNormals);
            tmpMeshRender.mesh.SetUVs(0, tmpListUVs);
            tmpMeshRender.mesh.RecalculateBounds();

            tmpListVertices.Clear();
            tmpListVertices = null;

            tmpListTriangles.Clear();
            tmpListTriangles = null;

            tmpListNormals.Clear();
            tmpListNormals = null;

            tmpListUVs.Clear();
            tmpListUVs = null;
            arraySamplePosition = null;
         }
      }

      private void FillSamplePositions()
      {
         var tmpMaxDistanceZ = 0f;

         for(int i = 0; i < meshToFillSpline.vertices.Length; i++)
            if(meshToFillSpline.vertices[i][2] > tmpMaxDistanceZ)
               tmpMaxDistanceZ = meshToFillSpline.vertices[i][2];

         var tmpSamplesQuantity = Mathf.CeilToInt(refCinemachineSmoothPath.PathLength / tmpMaxDistanceZ);
         arraySamplePosition = new SamplePositionAndRotation[tmpSamplesQuantity + 1];

         var tmpIndexSamplePosition = 0;

         for(float i = 0; i < refCinemachineSmoothPath.PathLength; i += tmpMaxDistanceZ)
         {
            var tmpPositionAtUnit = refCinemachineSmoothPath.EvaluatePositionAtUnit(i, CinemachinePathBase.PositionUnits.Distance);
            var tmpRotationAtUnit = refCinemachineSmoothPath.EvaluateOrientationAtUnit(i, CinemachinePathBase.PositionUnits.Distance);
            arraySamplePosition[tmpIndexSamplePosition] = new SamplePositionAndRotation(tmpPositionAtUnit, tmpRotationAtUnit);
            tmpIndexSamplePosition++;
         }

         arraySamplePosition[^1] = new SamplePositionAndRotation(refCinemachineSmoothPath.EvaluatePositionAtUnit(refCinemachineSmoothPath.PathLength, CinemachinePathBase.PositionUnits.Distance), refCinemachineSmoothPath.EvaluateOrientationAtUnit(refCinemachineSmoothPath.PathLength, CinemachinePathBase.PositionUnits.Distance));
      }

      private void CreateSubdividedMesh()
      {
         arrayGameObjectsSegmentsMeshFilters = new GameObject[quantitySubdividedMeshs];

         for(int i = 0; i < arrayGameObjectsSegmentsMeshFilters.Length; i++)
         {
            arrayGameObjectsSegmentsMeshFilters[i] = new GameObject("MeshFilterFragment", typeof(MeshFilter), typeof(MeshRenderer));
            arrayGameObjectsSegmentsMeshFilters[i].GetComponent<MeshRenderer>().material = materialSpline;
            arrayGameObjectsSegmentsMeshFilters[i].transform.SetParent(transform);
         }
      }

      private void OnDrawGizmos()
      {
         if(!debug)
            return;

         for(int i = 0; i < arraySamplePosition.Length; i++)
         {
            var tmpPositionOnPath = arraySamplePosition[i].position;
            var tmpRotationVertex = arraySamplePosition[i].rotation;
            Debug.DrawLine(tmpPositionOnPath, tmpPositionOnPath + tmpRotationVertex * Vector3.up);
            Gizmos.DrawSphere(refCinemachineSmoothPath.EvaluatePositionAtUnit(i, CinemachinePathBase.PositionUnits.Distance), 0.15f);
         }

         foreach(Transform tmpChild in transform)
         {
            var tmpMeshRender = tmpChild.GetComponent<MeshRenderer>();
            var tmpBounds = tmpMeshRender.bounds;
            Gizmos.DrawWireCube(tmpBounds.center, tmpBounds.size);
         }
      }
   }

   [Serializable]
   public struct SamplePositionAndRotation
   {
      public Vector3 position;

      public Quaternion rotation;

      public SamplePositionAndRotation(Vector3 argPosition, Quaternion argRotation)
      {
         position = argPosition;
         rotation = argRotation.normalized;
      }
   }
}