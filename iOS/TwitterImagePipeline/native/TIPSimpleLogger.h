
// this is needed so that we can get past the issue with binding varadic protocol members

@interface TIPSimpleLogger : NSObject<TIPLogger>

- (void)tip_logWithLevel:(TIPLogLevel)level file:(nonnull NSString *)file function:(nonnull NSString *)function line:(int)line format:(nonnull NSString *)format, ... NS_FORMAT_FUNCTION(5,6);
- (void)tip_logWithLevel:(TIPLogLevel)level file:(nonnull NSString *)file function:(nonnull NSString *)function line:(int)line message:(nonnull NSString *)message;

@end
