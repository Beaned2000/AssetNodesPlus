using System;
using System.Collections.Generic;
using System.Text;
using FrooxEngine.LogiX.ProgramFlow;
using FrooxEngine;
using FrooxEngine.UIX;

namespace FrooxEngine.LogiX.ProgramFlow
{
    [Category(new string[] { "LogiX/Assets" })]
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

    public class CreateAssetLoader<A> : LogixNode where A : class, IAsset

    {
        //Inputs
        public readonly Input<IAssetProvider<A>> Provider;
        public readonly Input<Slot> slot;

        //Outputs
        public readonly Impulse OnDone;
        public readonly Impulse OnFail;

        [ImpulseTarget]

        public void Run()
        //runs when it gets an impulse
        {
            Slot target = slot.Evaluate();
            var Asset = Provider.EvaluateRaw();

            if (target != null & Asset != null)
            {
                //Attatches a new AssetLoader with the given Audioclip
                target.AttachComponent<AssetLoader<A>>().Asset.Value = Asset.ReferenceID;

                OnDone.Trigger();
            }
            else
            {
                //Trigger when no slot was found
                OnFail.Trigger();
            }
        }


    }
}
