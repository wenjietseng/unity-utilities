using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SphericalCoordinatesConvert
{
    public class SphericalCoordinates
    {
        /// <summary>
        /// Store spherical coordinate, spherical to cartesian and vice versa
        /// http://en.wikipedia.org/wiki/Spherical_coordinate_system
        /// https://blog.nobel-joergensen.com/2010/10/22/spherical-coordinates-in-unity/
        /// </summary>

        public float radius;    // radius >= 0, from a fixed origin
        public float polar;     // azimuth angle (in radian)
        public float elevation; // elevation angle (in radian)

        public Vector3 ToCartesian()
        {
            return SphericalToCartesian(radius, polar, elevation);
        }

        public static SphericalCoordinates ToSpherical(Vector3 cartCoord)
        {
            SphericalCoordinates store = new SphericalCoordinates();
            CartesianToSpherical(cartCoord, out store.radius, out store.polar, out store.elevation);
            return store;
        }

        public static Vector3 SphericalToCartesian(float radius, float polar, float elevation)
        {
            Vector3 outCart = new Vector3();
            float a = radius * Mathf.Cos(elevation);
            outCart.x = a * Mathf.Cos(polar);
            outCart.y = radius * Mathf.Sin(elevation);
            outCart.z = a * Mathf.Sin(polar);
            return outCart;
        }

        public static void CartesianToSpherical(Vector3 cartCoord, out float outRadius, out float outPolar, out float outElevation)
        {
            if (cartCoord.x == 0) cartCoord.x = Mathf.Epsilon;
            outRadius = Mathf.Sqrt(cartCoord.x * cartCoord.x + cartCoord.y * cartCoord.y + cartCoord.z * cartCoord.z);
            outPolar = Mathf.Atan2(cartCoord.z, cartCoord.x); // otherwise x and z would have negative
            if (cartCoord.x < 0) outPolar += Mathf.Epsilon;
            outElevation = Mathf.Asin(cartCoord.y / outRadius);
        }
    }
    // // sepherical coord to cartesian coord (old)
    // // x -> x, y -> z, z -> y (Wiki to Unity)
    // Vector3 realHand2origin = realHand.position - origin.position;
    // // working on the correctness of this GOGO
    // // Perhaps GOGO is not good for the translation in our case;
    // phi = Mathf.Atan2(realHand2origin.z, realHand2origin.x);
    // theta = Mathf.Acos(realHand2origin.y / R_real);
    // float x = R_virtual * Mathf.Sin(theta) * Mathf.Cos(phi);
    // float z = R_virtual * Mathf.Sin(theta) * Mathf.Sin(phi); // wrong
    // float y = R_virtual * Mathf.Cos(theta);
    // virtualHand.transform.localPosition = new Vector3(x, y, z); // consider localPosition, different from all global variables, did not consider rotation/eulerAngle/quaternion
}