using System;
using System.Collections.Generic;
using System.Text;
using FrooxEngine.LogiX.ProgramFlow;
using FrooxEngine;
using FrooxEngine.UIX;

namespace FrooxEngine.LogiX.ProgramFlow
{
    [Category("LogiX/Assets")]
    [NodeName("GetIAssetProvider")]

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
    public class GetIAssetProvider<A> : LogixNode where A : class, IAsset
    {
        //Inputs
        public readonly Input<Slot> slot;

        //Outputs
        public readonly Impulse OnDone;
        public readonly Impulse OnFail;
        public readonly Output<IAssetProvider<A>> AssetProviderOut;

        [ImpulseTarget]
        public void Run()
        {
            Slot target = slot.Evaluate();

            if (target != null)
            {

                //Get the first  available IAssetProvider component on the slot
                var AssetComponent = target.GetComponent<IAssetProvider<A>>();
                AssetProviderOut.Value = AssetComponent;

                //Check if it found an IAssetprovider and trigger OnDone if yes otherwise trigger an OnFail
                if (AssetComponent != null) OnDone.Trigger();
                else OnFail.Trigger();
            }
            else

            {
                OnFail.Trigger(); //Trigger an impulse if no slot was found failing
            }
            AssetProviderOut.Value = null; //Set the output value to null again
        }
    }
}
