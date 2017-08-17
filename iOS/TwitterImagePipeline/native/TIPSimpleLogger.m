
// this is needed so that we can get past the issue with binding varadic protocol members

@implementation TIPSimpleLogger

- (void)tip_logWithLevel:(TIPLogLevel)level file:(nonnull NSString *)file function:(nonnull NSString *)function line:(int)line format:(nonnull NSString *)format, ... NS_FORMAT_FUNCTION(5,6) {
    // create the nice string
    va_list arguments;
    va_start(arguments, format);
    NSString *message = [[NSString alloc] initWithFormat:format arguments:arguments];
    va_end(arguments);

    // call the method that Xamarin can understand
    [self tip_logWithLevel: level file: file function: function line: line message: message];
}

- (void)tip_logWithLevel:(TIPLogLevel)level file:(nonnull NSString *)file function:(nonnull NSString *)function line:(int)line message:(nonnull NSString *)message {
}

@end
