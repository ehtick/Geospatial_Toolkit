﻿using BH.oM.Geospatial;
using BH.oM.Reflection.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.Engine.Geospatial
{
    public static partial class Query
    {
        /***************************************************/
        /****           Public Methods                  ****/
        /***************************************************/

        [Description("Method for querying an IGeospatial object for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int IUTMZone(IGeospatial geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            return UTMZone(geospatial as dynamic);
        }

        /***************************************************/

        [Description("Convert longitude to Universal Transverse Mercator zone.")]
        [Input("longitude", "The longitude to convert, in the range -180.0 to 180.0 with up to 7 decimal places.")]
        [Output("utmZone", "Universal transverse Mercator zone.")]
        public static int UTMZone(this double longitude)
        {
            return (int)Math.Floor((longitude + 180) / 6) + 1;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial object for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this Point geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            return UTMZone(geospatial.Longitude);
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial object for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this BoundingBox geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            return (UTMZone(geospatial.Min) + UTMZone(geospatial.Max))/2;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial MultiPoint for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this MultiPoint geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach(Point p in geospatial.Points)
                zone += UTMZone(p.Longitude);
            return (int)zone / geospatial.Points.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial LineString for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this LineString geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (Point p in geospatial.Points)
                zone += UTMZone(p);
            return (int)zone / geospatial.Points.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial MultiLineString for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this MultiLineString geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (LineString lineString in geospatial.LineStrings)
                zone += UTMZone(lineString);
            return (int)zone / geospatial.LineStrings.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial Polygon for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this Polygon geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (LineString lineString in geospatial.Polygons)
                zone += UTMZone(lineString);
            return (int)zone / geospatial.Polygons.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial MultiPolygon for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this MultiPolygon geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (Polygon polygon in geospatial.Polygons)
                zone += UTMZone(polygon);
            return (int)zone / geospatial.Polygons.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial Feature for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this Feature geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            return UTMZone(geospatial.Geometry as dynamic); 
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial FeatureCollection for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this FeatureCollection geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (Feature feature in geospatial.Features)
                zone += UTMZone(feature.Geometry as dynamic);
            return (int)zone / geospatial.Features.Count;
        }

        /***************************************************/

        [Description("Method for querying an IGeospatial GeometryCollection for its zone in the Universal Transverse Mercator Projection.")]
        [Input("geospatial", "IGeospatial object to query.")]
        [Output("zone", "The UTM zone. If the input IGeospatial includes objects that span multiple zones the average zone is returned.")]
        public static int UTMZone(this GeometryCollection geospatial)
        {
            if (geospatial == null)
            {
                Reflection.Compute.RecordError("Cannot query a null geospatial object.");
                return -1;
            }
            int zone = 0;
            foreach (IGeospatial feature in geospatial.Geometries)
                zone += UTMZone(feature as dynamic);
            return (int)zone / geospatial.Geometries.Count;
        }

        /***************************************************/
        /****           Private Fallback Method         ****/
        /***************************************************/
        private static int UTMZone(IGeospatial geospatial)
        {
            Reflection.Compute.RecordWarning("UTM zone could not be found.");
            return 0;
        }
    }
}
