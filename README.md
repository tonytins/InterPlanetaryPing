# InterPlanetary Ping

InterPlanetary Ping is a simple .NET 6 Worker Service for macOS, Linux, and Windows that helps to ensure that your IPFS CIDs don’t “age out” of the system in 30 days. It does this by regularly pinging the CIDs, making sure they stay active, even when you are unable to access the client where they originated from.

For me, IPFS functions as a CDN for certain content-heavy websites, such as my Document of Cartoons (DOC) project that hosts all my old artwork, which makes this project vital to have. While the DOC’s content is also hosted locally on the server, fetching from IPFS saves bandwidth because of how regularly it dynamically populates the gallery.

## Status

InterPlanetary Ping is currently in the prototyping stages. While the software works, it only pings the CIDs that I have hosted. This limitation means that it currently serves as a proof-of-concept rather than a fully-featured tool.

My current focus is on setting up the proper means of configuration, which will allow InterPlanetary Ping to ping any authorized CID, not just the ones that I have hosted. Once this functionality is implemented, InterPlanetary Ping will be a powerful and versatile tool for IPFS content hosting.

If you're interested in the project, be sure to check back regularly for updates on its progress.

<!-- ## How it Works

InterPlanetary Ping allows you to set a delay and have as many CIDs listed as you want. The service will then regularly ping the listed CIDs to keep them active. It is a straightforward tool, but it provides an essential function for those who rely on IPFS for content hosting. -->

## Installation

To install InterPlanetary Ping, you’ll need to have .NET Core 6 or later installed on your system. Once you have that installed, you can clone the repository and run the following commands:

bash

``` bash
cd InterPlanetaryPing
dotnet build
```

This will build the project and create an executable file that you can run on your system.

<!-- ## Usage

To use InterPlanetary Ping, simply run the executable file and specify the delay and CIDs that you want to ping. For example:

```bash
./InterPlanetaryPing --delay 60 --cids Qm123abc Qm456def Qm789ghi
```

This will ping the specified CIDs every 60 seconds, ensuring that they stay active in the IPFS network. -->

## Security

Security is of utmost importance to me when dealing with software, and InterPlanetary Ping is no exception. Unless I discover a feature that requires a certain bleeding-edge feature, it will always target the latest target .NET LTS. This ensures that the software is always using the most up-to-date and secure version of the .NET framework.

Due to the simplistic nature of the software, most updates will be dependency-related unless a bug is discovered. This approach minimizes the risk of introducing new security vulnerabilities while keeping the software up-to-date and performing optimally.

Finally, it is crucial to ensure that you are only pinging CIDs that belong to you or that you have permission to ping. Pinging CIDs that do not belong to you can potentially expose your system to security vulnerabilities.

## License

InterPlanetary Ping is licensed under the GPL, which means that it is free and open source software. You are free to use, modify, and distribute this software as long as you adhere to the terms of the GPL.

## Contributing

If you find a bug or have a suggestion for how to improve InterPlanetary Ping, please feel free to open an issue or submit a pull request on GitHub. I welcome contributions from anyone who is interested in helping to improve this project.