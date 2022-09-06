using System;
using System.Collections.Generic;
using FrooxEngine.LogiX.ProgramFlow;
using FrooxEngine;
using FrooxEngine.UIX;
using BaseX;
using System.Linq;

namespace FrooxEngine.LogiX.ProgramFlow
{

    [Category("LogiX/Assets")]
    [NodeName("GetAssetURL")]

    //Make a group for the generic types of assets
    [GenericTypes(new Type[]
{
    typeof(Texture2D),
    typeof(Texture3D),
    typeof(VideoTexture),
    typeof(AudioClip),
    typeof(ITexture2D),
    typeof(Mesh),
    typeof(LocaleResource)
})]
    public class GetAssetURL<A> : LogixNode where A : class, IAsset
    {
        //Inputs
        public readonly Input<IAssetProvider<A>> Provider;

        //Outputs
        public readonly Impulse OnDone;
        public readonly Impulse OnFail;
        public readonly Output<Uri> Url;

        [ImpulseTarget]
        public void Run()
        //Run when it gets an impulse
        {
            var Asset = Provider.Evaluate();

            if (Asset != null)
            {
                IField URL = Asset.TryGetField("URL"); //Try to get the field on the Asset called "URL"
                Uri result;
                Url.Value = Uri.TryCreate(URL.ToString(), UriKind.Absolute, out result) ? result : (Uri)null; //tries to convert the Ifield to a Uri variable, fail will result in null
                OnDone.Trigger();   //incase the code above hasn't thrown multiple exeptions this should fire
            }
            else
            {
                OnFail.Trigger();   //when it tries its best but it don't succeed
            }

            Url.Value = null; //Set value back to null after the action
        }

        protected override void NotifyOutputsOfChange()
        {
            ((IOutputElement)Url).NotifyChange();
        }
    }
}
