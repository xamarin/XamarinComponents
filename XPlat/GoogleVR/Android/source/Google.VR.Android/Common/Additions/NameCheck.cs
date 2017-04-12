using System;
namespace Common
{
    internal class NameCheck
    {
        private NameCheck ()
        {
            // When https://bugzilla.xamarin.com/show_bug.cgi?id=43440 is fixed I expect this to break.
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.AudioStats.RenderingModeConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.Cyclops.Capture.OutcomeConsts.Unknown); 
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.Cyclops.Share.TypeConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.Cyclops.ShareStart.OriginScreenConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.Cyclops.View.OrientationConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.Cyclops.View.OrientationConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.EarthVr.ControllerState.RoleConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.EmbedVrWidget.ViewModeConsts.UnknownMode);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.QrCodeScan.StatusConsts.Unknown);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.VrCore.ErrorCodeConsts.UnknownError);
            IgnoreWarning (Google.Common.Logging.Nano.VrVREvent.VrCore.PermissionConsts.UnknownPermission);

            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosFieldDescriptorProto.LabelConsts.LabelOptional);
            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosFieldDescriptorProto.TypeConsts.TypeBool);
            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosMethodOptions.LogLevelConsts.LogNone);
            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosMethodOptions.ProtocolConsts.Tcp);
            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosMethodOptions.SecurityLevelConsts.None);
            IgnoreWarning (Google.Protobuf.Nano.DescriptorProtosStreamOptions.TokenUnitConsts.Byte);




        }

        void IgnoreWarning (int ignoreMe) { }
    }
}

