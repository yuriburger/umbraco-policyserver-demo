# umbraco-policyserver-demo
[logo]: https://github.com/yuriburger/umbraco-policyserver-demo/blob/master/Feature.png "Umbraco Loves PolicyServer!"
![alt text][logo]

This source code is part of a blogpost about running managing identities in Umbraco BackOffice using IdentityServer and PolicyServer. More info: [Managing External Identities in Umbraco BackOffice with PolicyServer](https://yuriburger.net/2018/02/16/managing-external-identities-in-umbraco-backoffice-with-policyserver/)

To run the demo:

1.	Clone the repo;
2.	Open in Visual Studio;
3.	Restore packages (actually just wait a bit until Visual Studio finishes);
4.	Check and correct the ports and hostnames;

* IdentityServer is expected to run on http://localhost:5000
* PolicyServer is expected to run on http://localhost:5001
* UmbracoCms is expected to run on http://localhost:5003

5. Configure the multiple startup projects.

Now it is time to hit Ctrl-F5. Umbraco will launch the initial setup wizard, so you will need to complete this.




