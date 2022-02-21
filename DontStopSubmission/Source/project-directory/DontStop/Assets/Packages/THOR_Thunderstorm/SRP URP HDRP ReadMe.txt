If you are not using SRP/URP/HDRP you can ignore this ReadMe!

Since SRP/URP/HDRP use a different light falloff you need to increase the light intensity multi in the "Light Settings" to a very high value(and i mean really very high). Enable "Show Advanced Settings" in the Thor Thunderstorm Inspector to access the "Light Settings".


Only necessary for the demo scene:

The included terrain material is using the standard shader (built-in renderpipeline) and the material needs to be upgraded for usage in SRP/URP/HDRP. Please refer to the Unity's Render-Pipeline material upgrade workflow.