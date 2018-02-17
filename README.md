# texelfx

A larger project to provide an easy way to scale standard definition images or video to HD (and possibly 4k).  At first I was going to use Microsoft's CNTK framework, but this has issues with .NET Core 2 at the moment.  So I am using Tensorflow with my .NET Core 2 port of TensorflowSharp.

Rather than tackle that entire problem, I broke it into 3 versions.

First Version:

-Detection of Compression Artifacts (JPEG/MPEG)

-Detection of the pixels that exhibit the compression artifacts

-Removal of Compression Artifacts

Second Version:

-Detection of detail loss due to resolution

-Replacement of pixels due to resolution from a higher definition source

Third Version:
-Combine both versions above to scale content to the desired resolution

## Blog Posts
http://www.jarredcapellman.com/2018/1/7/deep_dive_into_image_scaling_with_machine_learning_part_one
