# Drawing on textures at runtime with GPU in UNITY
This scripts (and a shader) allows you to draw on textures at runtime with lightning-fast speed, unlike if you trying to do the same on CPU using `GetPixels` and `SetPixels`.

## In action
![Preview](/images/preview1.gif)

**Stable drawing at *high FPS* (1000+ on Nvidia GTX 1060). Same result, but using CPU to draw runs at 10-15 FPS.**

Also, there is an example with a "scratch" shader:

![Preview](/images/preview2.gif)

## Usage
All essential scripts and shader lies under `Scripts` and `Shaders` folders. Brushes lies under `Textures/brushes` folder.

### Scripts
- Put `MouseDrawController.cs` script on your camera, set **brush texture**, **color** and **size** in it
- Put `DrawZone.cs` script on mesh that you want to draw on

You can also inherit `DrawZone.cs` and create your own behaviour of script, example given in `ScratchZone.cs`


### Brushes
Brushes are black/white images. White area draws, black doesnt.

### Mesh requirements
You have to unwrap your mesh properly, so *left bottom corner* of mesh must be (0, 0) on UVs, and the top right corner of mesh must be (1, 1).

Just like this:

![Preview](/images/unwrapping.png)

## Conclusion

Thanks for your attention! I will be pleasured to see any feedback :)