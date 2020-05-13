# Chromium CroNet  

Android and iOS bindings of Chromium CroNet library - networking stack for HTTP3 
(HTTP2 and QUIC) used in Chrome browser.


## Links / References

*   https://mvnrepository.com/artifact/org.chromium.net

*   https://developer.android.com/guide/topics/connectivity/cronet

*   https://chromium.googlesource.com/chromium/src/+/master/components/cronet/

*   https://www.gsrikar.com/2018/12/cronet-chromium-network-stack.html

*   https://github.com/lizhangqu/cronet

*   https://stackoverflow.com/questions/60541683/how-to-send-json-object-as-post-request-in-cronet

*   https://medium.com/@cchiappini/discover-cronet-4c7b4812407

*   https://medium.com/the-react-native-log/using-cronet-in-your-mobile-app-7dda3a89c132

*   flutter

    *   https://github.com/flutter/flutter/issues/17413

*   react native

    *   https://github.com/akshetpandey/react-native-cronet

*   cake for ios

    *   https://github.com/xamarin/XamarinComponents/blob/master/XPlat/Estimote/iOS/build.cake

    *   https://chromium.googlesource.com/external/github.com/grpc/grpc/+/chromium-deps/2016-07-19/src/objective-c/tests/Podfile



The simplest way is to integrate cronet and have a backend server or CDN or a proxy like cloudflare 
that supports QUIC. Most google & GCP services including Youtube support QUIC by default. 

Cloudflare, Akamai, and Fastly support QUIC but needs to be manually enabled.



