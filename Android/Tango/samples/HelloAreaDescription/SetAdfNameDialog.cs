using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

using ProjectTango.Service;

namespace HelloAreaDescription
{
    /// <summary>
    /// Queries the user for an ADF name, optionally showing the ADF UUID.
    /// </summary>
    public class SetAdfNameDialog : DialogFragment
    {
        private EditText mNameEditText;
        private TextView mUuidTextView;
        private ICallbackListener mCallbackListener;
        private Button mOkButton;
        private Button mCancelButton;

        public interface ICallbackListener
        {
            void OnAdfNameOk(string name, string uuid);

            void OnAdfNameCancelled();
        }

        public override void OnAttach(Activity activity)
        {
            base.OnAttach(activity);

            mCallbackListener = (ICallbackListener)activity;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            Cancelable = false;
            Dialog.SetTitle(Resource.String.set_name_dialog_title);

            var dialogView = inflater.Inflate(Resource.Layout.set_name_dialog, container, false);
            mOkButton = dialogView.FindViewById<Button>(Resource.Id.ok);
            mCancelButton = dialogView.FindViewById<Button>(Resource.Id.cancel);
            mNameEditText = dialogView.FindViewById<EditText>(Resource.Id.name);
            mUuidTextView = dialogView.FindViewById<TextView>(Resource.Id.uuidDisplay);

            mOkButton.Click += delegate
            {
                mCallbackListener.OnAdfNameOk(mNameEditText.Text, mUuidTextView.Text);
                Dismiss();
            };
            mCancelButton.Click += delegate
            {
                mCallbackListener.OnAdfNameCancelled();
                Dismiss();
            };
            mNameEditText.Text = Arguments.GetString(TangoAreaDescriptionMetaData.KeyName) ?? string.Empty;
            mUuidTextView.Text = Arguments.GetString(TangoAreaDescriptionMetaData.KeyUuid) ?? string.Empty;

            return dialogView;
        }
    }
}
