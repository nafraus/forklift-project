# Haptics Manager Pro for Unity OpenXR
XRHapticsManagerPro to expand upon the base functionality of sending haptic impulses in OpenXR for Unity. Allows the user to apply multiple haptic impulses to controllers, supports curves and custom types, and has different application types.

XRHapticsManagerPro solves the limitations of OpenXR's ability to send haptic feedback. It enables users to process overlapping impulses, add to, multiply to, and override haptic feedback, and supports Curves and custom types. If your project needs to process different feedback at the same time or needs support for non-float and curve feedback, XRHapticsManagerPro will solve those needs. Otherwise, I recommend using OpenXR's base functionality. 

## Installation
Download a ZIP file from this page, extract it, and import the folder into your Unity project.
A unity package for the preview release of XRHapticsManagerPro will come soon.

## Usage
1. Create a new GameObject in your scene named 'HapticsManager', and add the 'XRHapticsManagerPro" component. Find the left and right controller under your XR Origin, and add those to their respective fields in the XRHapticControllerSpecifier component.
2. Make a new folder in your project folder to store the haptic impulses you create, called 'HapticData'.
3. Create your first Haptic data by right clicking on your new folder, Create > ScriptableObject > HapticData > and pick one. See [HapticData](#hapticdata) for more
4. Create a new script or in an existing script add a field for a GenericHapticData. See SampleHapticManagerImplementation or the Impulse Tester prefab found in XRHapticsManagerPro > Samples. 
        
```c#
    public GenericHapticData Impulse;
```
5. Implement the XRHapticsManagerPro function call in your script `XRHapticsManagerPro.Instance.SendHapticImpulse()`, passing in Impulse and the controller you want to send the impulse. For example: 

``` 
public bool ActivateImpulse;

   private void Update()
    {
        if (ActivateImpulse)
        {
            ActivateImpulse = false;
        
            //Sends a haptic impulse to both conrtollers
            XRHapticsManagerPro.Instance.SendHapticImpulse(Data, XRHapticControllerSpecifier.both);
        }
   }
```

6. Save your script, and go back into Unity. Add your HapticData to the field you created in step 4, and you are done!

## XRHapticsManagerPro

The XRHapticsManagerPro class is a singleton that stores haptic impulses, performs calculations, and sends haptic impulses to the XR controllers. 

</br></br>SendHapticImpulse(): This function with three different method overloads is used to send HapticData to the XRHapticsManagerPro instance. When the function is called, XRHapticsManagerPro will generate a HapticImpulse from the HapticData, store it and subscribe the add time and removal events, and use it for further processing. 
</br>SendHapticImpulse(HapticData, XRDirectInteractor): Most efficient way to interface with XRHapticsManagerPro, sends impulse to that direct interactor
</br>SendHapticImpulse(HapticData, XRBaseInteractable): Passing in an XRBaseInteractable will send haptic feedback to all controllers that are selecting that object, when the function is called.
</br>SendHapticImpulse(HapticData, XRBaseController): Passing in the XRBaseController will send haptic impulse to the direct interactor of this controller.
</br>SendHapticImpulse(HapticData, XRHapticControllerSpecifier): XRHapticControllerSpecifier is an enum provided that allows the user to send haptics to the left, right or both controllers without needing a reference.

</br></br> RemoveHaptic(HapticData): Removes all impulses that contain the given HapticData from the stored haptic impulses. Planned for future update and overloads.

## HapticData
HapticData is a typed scriptable object that the user creates to store information about the haptic impulse the user wants to send. Users will use the GenericHapticData class when creating fields in the inspector, and should not need to alter the HapticData class. XRHapticsManagerPro comes with FloatHapticData and CurveHapticData built in. See [Custom Types](#custom-types) to learn how to support other types. HapticData uses GenerateImpulse to generate a [HapticImpulse](#hapticimpulse) with writable data that is used by then XRHapticsManagerPro class.

The calculated impulse sent to the controllers is clamped from 0-1. Make sure to account for this when creating HapticData.

- Intensity: Dynamic type that will determine the strength of the haptic impulse at any given time.
- Type: Modifier type that changes how this haptic data will interact with other haptic data.
  - Additive: Adds itself to all other additives
  - Multiplier: Multiplies itself to all other multipliers, then multiplies with additives.
  - Override: Overrides all other active HapticData, and only using the applying the most recent override haptic data for each DirectInteractor.
- Duration: How long the haptic impulse will be active for.

HapticImpulse GenerateImpulse(): Generates an instance of HapticImpulse using the data set in HapticData. 

## HapticImpulse
HapticImpulse is an abstract class, implemented to HapticImpulse classes with a type (e.g. FloatHapticImpulse). The class is writable and contains a HapticData reference, the current duration of which it has been running, and a property returning the current float value of the impulse. HapticImpulse subscribes itself to an event in XRHapticsManagerPro to add time to each impulse, and contains a removal event and subscribes XRHapticsManagerPro to. HapticImpulse is generated by HapticData using `HapticData.GenerateImpulse()`.

## Custom Types
XRHapticsManagerPro was designed to use generic types for HapticData and Impulses, and allows the user to use types other than float and curve with minimal effort.

### To Create a Custom Type:
1. Find and duplicate 'ExampleHapticDataSO' and 'ExampleHapticImpulse' in XRHapticsManagerPro > Samples.
2. Rename both duplicated files and replace Example with the name of the new type you will be using.
3. Open both of the new files and follow the instructions left in the comments

</br> Detailed tutorial and images coming soon.

