# Upcoming Movies App
*Upcoming movies list and details from TMDb*

I focused my attention on the construction of a basic but scalable and decoupled architecture based on the Onion concept and some reference to the DDD, using some of the most relevant design patterns and development practices such as Clean Code, Fluent Code, MVVM, etc.

So don't expect to see an application with amazings color palette and icons library (by the way, I did not create any icon for the application). For this MVP, basic colors were used without defining implicit styles nor a stylesheet.

Despite being Mobile Cross Platform, the application was only tested on Android device / emulator and Windows 10.

## Globalization

The application has content in American English and Brazilian Portuguese, according to the device language settings.

## Development Environment:

- IDE: Visual Studio 2017

- Primary programming languages: C#, XAML.

- Primary development SDK: Xamarin Forms

- Third party components:

  - Newtonsoft Json: indispensable in the object serialization and deserialization processes and manipulation of json files;
  - Prism: includes dependency injection functionality, event aggregation and "page routing", streamlining and facilitating the MVVM    development process;
  - Unity: dependency container used by Prism. I'm also using it directly on some solution layers.
