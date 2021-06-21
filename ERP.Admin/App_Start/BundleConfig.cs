using System.Web.Optimization;

using BundleTransformer.Core.Builders;
using BundleTransformer.Core.Orderers;
using BundleTransformer.Core.Resolvers;
using BundleTransformer.Core.Transformers;

namespace ERP.Admin
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;

            var nullBuilder = new NullBuilder();
            var styleTransformer = new StyleTransformer();
            var scriptTransformer = new ScriptTransformer();
            var nullOrderer = new NullOrderer();

            // Replace a default bundle resolver in order to the debugging HTTP-handler
            // can use transformations of the corresponding bundle
            BundleResolver.Current = new CustomBundleResolver();


            //Style Bundles
            var bootstrapStylesBundle = new Bundle("~/css/bootstrap");
            bootstrapStylesBundle.Include(
                "~/assets/css/bootstrap.min.css");
            bootstrapStylesBundle.Builder = nullBuilder;
            bootstrapStylesBundle.Transforms.Add(styleTransformer);
            bootstrapStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapStylesBundle);

            var bootstrapRtlStylesBundle = new Bundle("~/css/bootstrap-rtl");
            bootstrapRtlStylesBundle.Include(
                "~/assets/css/bootstrap-rtl.min.css");
            bootstrapRtlStylesBundle.Builder = nullBuilder;
            bootstrapRtlStylesBundle.Transforms.Add(styleTransformer);
            bootstrapRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapRtlStylesBundle);

            var beyondStylesBundle = new Bundle("~/css/beyond");
            beyondStylesBundle.Include(
                "~/assets/css/beyond.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/css/animate.min.css");
            beyondStylesBundle.Builder = nullBuilder;
            beyondStylesBundle.Transforms.Add(styleTransformer);
            beyondStylesBundle.Orderer = nullOrderer;
            bundles.Add(beyondStylesBundle);

            var beyondRtlStylesBundle = new Bundle("~/css/beyond-rtl");
            beyondRtlStylesBundle.Include(
                "~/assets/css/beyond-rtl.min.css",
                "~/assets/css/demo.min.css",
                "~/assets/css/font-awesome.min.css",
                "~/assets/css/typicons.min.css",
                "~/assets/css/weather-icons.min.css",
                "~/assets/css/animate.min.css");
            beyondRtlStylesBundle.Builder = nullBuilder;
            beyondRtlStylesBundle.Transforms.Add(styleTransformer);
            beyondRtlStylesBundle.Orderer = nullOrderer;
            bundles.Add(beyondRtlStylesBundle);

            //var swalStyleBundle = new Bundle("~/css/swal");
            //swalStyleBundle.Include(
            //    "~/assets/css/sweetalert.css");
            //swalStyleBundle.Builder = nullBuilder;
            //swalStyleBundle.Transforms.Add(styleTransformer);
            //swalStyleBundle.Orderer = nullOrderer;
            //bundles.Add(swalStyleBundle);

            var swalStyleBundle = new Bundle("~/css/swal");
            swalStyleBundle.Include(
                "~/assets/css/sweet2/sweetalert2.css",
                "~/assets/css/sweet2/sweetalert2.min.css");
            swalStyleBundle.Builder = nullBuilder;
            swalStyleBundle.Transforms.Add(styleTransformer);
            swalStyleBundle.Orderer = nullOrderer;
            bundles.Add(swalStyleBundle);

            //Script Bunldes
            var skinBundle = new Bundle("~/bundles/skin");
            skinBundle.Include("~/assets/js/skins.min.js");
            skinBundle.Builder = nullBuilder;
            skinBundle.Transforms.Add(scriptTransformer);
            skinBundle.Orderer = nullOrderer;
            bundles.Add(skinBundle);

            var jQueryBundle = new Bundle("~/bundles/jquery");
            jQueryBundle.Include("~/assets/js/jquery.min.js");
            jQueryBundle.Builder = nullBuilder;
            jQueryBundle.Transforms.Add(scriptTransformer);
            jQueryBundle.Orderer = nullOrderer;
            bundles.Add(jQueryBundle);

            

            var bootstrapBundle = new Bundle("~/bundles/bootstrap");
            bootstrapBundle.Include(
                  "~/assets/js/bootstrap.min.js",
                "~/assets/js/slimscroll/jquery.slimscroll.min.js");
            bootstrapBundle.Builder = nullBuilder;
            bootstrapBundle.Transforms.Add(scriptTransformer);
            bootstrapBundle.Orderer = nullOrderer;
            bundles.Add(bootstrapBundle);

            var beyondBundle = new Bundle("~/bundles/beyond");
            beyondBundle.Include(
                  "~/assets/js/beyond.min.js");
            beyondBundle.Builder = nullBuilder;
            beyondBundle.Transforms.Add(scriptTransformer);
            beyondBundle.Orderer = nullOrderer;
            bundles.Add(beyondBundle);


            var jQueryValBundle = new Bundle("~/bundles/jqueryval");
            jQueryValBundle.Include(
                 "~/assets/js/jqueryval/jquery.validate*");
            jQueryValBundle.Builder = nullBuilder;
            jQueryValBundle.Transforms.Add(scriptTransformer);
            jQueryValBundle.Orderer = nullOrderer;
            bundles.Add(jQueryValBundle);

            //var swalBundle = new Bundle("~/bundles/swal");
            //swalBundle.Include("~/assets/js/sweetalert.min.js",
            //    "~/assets/js/sweetalert-dev.js");
            //swalBundle.Builder = nullBuilder;
            //swalBundle.Transforms.Add(scriptTransformer);
            //swalBundle.Orderer = nullOrderer;
            //bundles.Add(swalBundle);

            var swalBundle = new Bundle("~/bundles/swal");
            swalBundle.Include("~/assets/js/sweet2/sweetalert2.min.js",
                "~/assets/js/sweet2/sweetalert2.js",
                "~/assets/js/sweet2/sweetalert2.common.js",
                "~/assets/js/sweet2/sweetalert2.common.min.js");
            swalBundle.Builder = nullBuilder;
            swalBundle.Transforms.Add(scriptTransformer);
            swalBundle.Orderer = nullOrderer;
            bundles.Add(swalBundle);

            bundles.Add(new StyleBundle("~/plugins/fullCalendarStyles").Include(
                      "~/Content/fullcalendar/fullcalendar.css"));

            // fullCalendar 
            bundles.Add(new ScriptBundle("~/plugins/fullCalendar").Include(
                      "~/Scripts/fullcalendar/moment.min.js",
                      "~/Scripts/fullcalendar/fullcalendar.min.js"));
            bundles.Add(new ScriptBundle("~/plugins/dateRange").Include(
                      // Date range use moment.js same as full calendar plugin 
                      "~/Scripts/fullcalendar/moment.min.js",
                      "~/Scripts/daterangepicker/daterangepicker.js"));
        }
    }
}