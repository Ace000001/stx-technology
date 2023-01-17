using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.WebAssets;
using Umbraco.Cms.Core;

namespace TouchSystems.Helper
{
    public class CreateBundlesNotificationHandler : INotificationHandler<UmbracoApplicationStartingNotification>
    {
        private readonly IRuntimeMinifier _runtimeMinifier;
        private readonly IRuntimeState _runtimeState;

        public CreateBundlesNotificationHandler(IRuntimeMinifier runtimeMinifier, IRuntimeState runtimeState)
        {
            _runtimeMinifier = runtimeMinifier;
            _runtimeState = runtimeState;
        }
        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            if (_runtimeState.Level == RuntimeLevel.Run)
            {
                _runtimeMinifier.CreateJsBundle("registered-js-bundle",
                    BundlingOptions.OptimizedNotComposite,
                    new[] { 
                        "~/plugins/jquery.min.js",
                        "~/plugins/nouislider/nouislider.min.js",
                        "~/plugins/popper.min.js",
                        "~/plugins/owl-carousel/owl.carousel.min.js",
                        "~/plugins/bootstrap/js/bootstrap.min.js",
                        "~/plugins/imagesloaded.pkgd.min.js",
                        "~/plugins/masonry.pkgd.min.js",
                        "~/plugins/isotope.pkgd.min.js",
                        "~/plugins/jquery.matchHeight-min.js",
                        "~/plugins/slick/slick/slick.min.js",
                        "~/plugins/jquery-bar-rating/dist/jquery.barrating.min.js",
                        "~/plugins/slick-animation.min.js",
                        "~/plugins/lightGallery-master/dist/js/lightgallery-all.min.js",
                        "~/plugins/sticky-sidebar/dist/sticky-sidebar.min.js",
                        "~/plugins/select2/dist/js/select2.full.min.js",
                        "~/plugins/gmap3.min.js",
                        "~/scripts/js/main.js"
                    });

                _runtimeMinifier.CreateCssBundle("registered-critical-css-bundle",
                    BundlingOptions.OptimizedNotComposite,
                    new[] {
                        "~/plugins/bootstrap/css/bootstrap.min.css",
                        "~/css/css/style.css",
                        "~/css/css/electronic.css"
                    });

                _runtimeMinifier.CreateCssBundle("registered-css-bundle",
                    BundlingOptions.OptimizedNotComposite,
                    new[] {
                        "~/plugins/nouislider/nouislider.min.css",
                        "~/plugins/font-awesome/css/font-awesome.min.css",
                        "~/css/fonts/Linearicons/Linearicons/Font/demo-files/demo.css",
                        "~/plugins/owl-carousel/assets/owl.carousel.min.css",
                        "~/plugins/slick/slick/slick.css",
                        "~/plugins/lightGallery-master/dist/css/lightgallery.min.css",
                        "~/plugins/jquery-bar-rating/dist/themes/fontawesome-stars.css",
                        "~/plugins/select2/dist/css/select2.min.css"
                    });
            }
        }
    }
}
