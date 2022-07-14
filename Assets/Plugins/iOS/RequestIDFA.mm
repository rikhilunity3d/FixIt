#import <AppTrackingTransparency/AppTrackingTransparency.h>
#import <AdSupport/AdSupport.h>

extern "C" void _requestIDFA() {
    if (@available(iOS 14.5,*)) {
        [ATTrackingManager requestTrackingAuthorizationWithCompletionHandler:^(ATTrackingManagerAuthorizationStatus status) {
            // Tracking authorization completed. Start loading ads here.
            // [self loadAd];
          }];
    }
}