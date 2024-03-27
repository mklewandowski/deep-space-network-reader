# deep-space-network-reader
DSN Reader is a small app that reads, reports, and visualizes real-time communication data from the NASA Deep Space Network. DSN Reader was originally created as a prototype demo for potential use in a larger educational radio science project or mobile application. Our team never pursued DSN Reader further so for now it is just a prototype demo.

## Design
DSN Reader uses an HTTP GET request to access a publically available XML file that contains information about the NASA Deep Space Network. This XML file is maintained by NASA and is located here: https://eyes.nasa.gov/dsn/data/dsn.xml. Once the XML file is downloaded, it is parsed and the network information is displayed to the user. Each terrestrial node in the DSN is represented in the XML file with a `<dish>` tag. Each `<dish>` tag may contain 1 or more `<upSignal>` and `<downSignal>` tags which describe the data transfer coming from and to the DSN node. DSN Reader pulls down the latest version of the XML file every 2 seconds and updates the interface based on the new DSN data.

## Supported Platforms
DSN Reader is designed for use on multiple platforms including:
- iOS
- Android
- Web
- Mac and PC standalone builds

## Running Locally
Use the following steps to run locally:
1. Clone this repo
2. Open repo folder using Unity 2021.3.35f1
3. Install Text Mesh Pro

## Development Tools
- Created using Unity
- Code edited using Visual Studio Code
- 2D images created and edited using [Paint.NET](https://www.getpaint.net/)
