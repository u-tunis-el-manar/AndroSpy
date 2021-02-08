using Android.App;
using Android.Content;
using Android.Content.PM;
using Task2;

namespace izci
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Priority = (int)IntentFilterPriority.HighPriority)]
    public class BootReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                var pckg = context.PackageManager;
                ComponentName componentName = new ComponentName(context, Java.Lang.Class.FromType(typeof(MainActivity)).Name);
                pckg.SetComponentEnabledSetting(componentName, ComponentEnabledState.Enabled, ComponentEnableOption.DontKillApp);
            }
            catch (System.Exception) { }
            Intent start = new Intent(context, Java.Lang.Class.FromType(typeof(MainActivity)));
            start.AddFlags(ActivityFlags.NewTask);
            context.StartActivity(start);

        }
    }
}