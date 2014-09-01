using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace WPF_RemoteControl_with_USBUIRT
{
    public class RCViewModel
    {
        public RCModel myRCModel;
        
        public RCViewModel(RCModel rcmodel)
        {
            this.myRCModel = rcmodel;
        }

        public void onPowerBtClick(object sender, RoutedEventArgs routedEventArgs)
        {
            this.myRCModel.Energy = !(this.myRCModel.Energy); 
        }

        public void onChUpBtClick(object sender, RoutedEventArgs routedEventArgs)
        {
            this.myRCModel.Channel++;
        }

        public void onChDownBtClick(object sender, RoutedEventArgs routedEventArgs)
        {
            this.myRCModel.Channel--;
        }

        public void onVolUpBtClick(object sender, RoutedEventArgs routedEventArgs)
        {
            this.myRCModel.Volume++;
        }

        public void onVolDownBtClick(object sender, RoutedEventArgs routedEventArgs)
        {
            this.myRCModel.Volume--;
        }
    }
}
