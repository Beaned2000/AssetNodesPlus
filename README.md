# BetterAssetNodes
A Neosvr plugin that adds a few handy nodes that are related to handling assets.
Current nodes it adds:
  - CreateAssetLoader
    -- A node that creates an AssetLoader on a slot that has an IAssetprovider on it so Neos doesn't get rid of it during assetcleanup
    
  - GetAssetProvider
    -- A node that gets the first IAssetprovider it can find on a slot
    
  - GetAssetURL
    -- A node that gets the Uri of the asset from a provided IAssetProvider input (this works together with the "GetAssetProvider" node)
