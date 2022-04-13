# Elastiflix

Elastiflix is a fictitious video streaming service company that provides a web-based user interface leveraging data from [The Internet Movie Database](https://tmdb.org/). Elastiflix provides a mechanism to query data from an indexed copy of TMDB data that sits in Elastic Cloud, allowing for high-performant, flexible management and querying of that data in order to provide an exceptional search experience.

## Pre-reqs

- [Elastic Cloud](https://cloud.elastic.co/) Deployment created, new accounts can sign-up for a limitless 14-day free trial to POC building a solution like this
- [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/downloads/) (Community is fine)

### Setup Elastic Cloud

To get started with this sample, create a 14-day free trial of Elastic Cloud by going to https://cloud.elastic.co/registration and signing up with your Google or Microsoft Account or by signing up with another email. After logging in, you will be prompted with the Elastic Cloud console. Clicking `Create Deployment` will walk you through creating a new Elastic Cloud deployment, hosted in the Cloud Provider and region of your choice. You can run Elastic Cloud in Microsoft Azure, Google Cloud Platform or Amazon Web Services. If you expand settings you can choose your provider/region as well as other settings, which we can leave default for now. When you create `Create Deployment` Elastic Cloud will create all the necessary components to build the solution. There will be a username and password shown during the provisioning process, so be sure to save that somewhere safe as it is an admin account for your cluster. 

Once the cluster is ready, you can navigate to it at the link provided. This will take you to Kibana, the solution management and visualization tool for Elastic. You will be prompted to Add integrations or Explore on your own, which is the fastest way to get started with this sample. From there, you will be shown the solutions that Elastic provides to help folks innovate. Since we are building a search solution, let's click `Enterprise Search` and than `App Search. The next step we will do is create an engine to store the data that we will indexed and made available to search over. Once you create an engine, you are setup with everything you need to index data into Elastic. There are a few settings that you should capture and place in the sample to point it at this newly created Elastic Cloud instance. These settings are

- ElasticEngineName: Name of Engine that you created above
- ElasticTenantUrl: Navigte to the Credentials section via the left-hand navigation and copy the endpoint
- ElasticEngineToken: Token needed to read/write to the Elastic API, from the Credentials screen, copy the `private-key` API Key for this sample. In real-world scenarios you should create different API keys for different needs.

### The Internet Movie Database API

To pull movies from The Internet Movie Database, you will need to create a free developer account to make use of the API. You can do that by following the steps outlined [here](https://developers.themoviedb.org/3/getting-started/introduction). Once you do that, save the APIKey so you can add it to the sample.

- TMDBToken: API Key to access The Internet Movie Database API

Once you have this information stored, you can open the solution in Visual Studio and get started.

## Parts to this sample


### Data Ingestion

Elastiflix has created a dataset in Elastic Cloud, called an engine, that will represent the indexed copy of the the data mentioned above. This data is indexed into Elastic using a .NET 6 console application in this case(for a more scalable solution that can be configured to sync data over time, a serverless technology like Azure Functions would facilitate this need). The entry point for this index process is defined [here](https://github.com/isaacrlevin/Elastiflix/blob/main/src/TMDBImport/Program.cs) with the entire workflow residing in different files. In essence the process is as follows

- Leverage the TMDB API to capture Movie along with Credit information and store that list of movies into memory
- Index those movies into Elastic Cloud using the Elastic APIs

**NOTE: To control the amount of movies that you want to index, you can update the `moviesToDownload` variable. The app chunks that number into groups of 10 and performs parallel API calls. The larger the `moviesToDownload` varible, the longer this process will take.**

To run the app, you will take the settingds captured above and place them in the `appsettings.json` file of hte `TMDBImport` project. 

Running the console app will build a list of movies and index them into the Elastic API in Elastic Cloud. You can monitor the ingestion of the data in the Elastic Cloud console by going to the Overview section via the left-hand navigation and look at the `Total Documents` Count as well the `Recent API events`.

Once the console app is complete, you can move onto the search experience of Elastiflix.

### Search Experience

Elastiflix has built a web-frontend in .NET 6, leveraging the ASP.NET Core [Blazor WebAssembly](https://docs.microsoft.com/en-us/aspnet/core/blazor/?view=aspnetcore-6.0#blazor-webassembly) framework. Since Blazor WebAssembly applications build into .NET assemblies partnered with the Blazor WebAsssembly framework, those assemblies get bootstrapped into WebAssembly in the browser, allowing the ability to write .NET applications without the need for a server. Since our "server" or place we get movie data from is the Elastic APIs, we have no need for the overhead to run a server, which allows for us to have a lightweight UI. 

To get the sample working, all we need to do is update the `appsettings.json` file of the `Web` project with the settings captured above. Once that is done, you can run the application. Once the application is running, you can use the search bar in the top right and get suggestions for your text  input. 

![Home Page](/static/home-page.png)

Clicking those links will take you to a results page, where you can search with more filters as well as sorting.

![Search Page](/static/search-page.png)


### What's next?

These 2 parts are just the start to building great search solutions, as Elastic also provides pre-built tools to help accelerate the building out of a top-notch search experience. You can find out more information about this at the [Elastic Enterprise Search](https://www.elastic.co/enterprise-search) section of the Elatic website.


Be sure to let me know if this sample is helpful or other things you would like to see. Also feel free to reach out to me on Twitter at [**@isaacrlevin**](https://twitter.com/isaacrlevin)


### Open Source Libraries

This solution makes use of a few Open Source libraries. I want to thank them for their contribution to the community. Be sure to check out their repos below

- [TMDbLib](https://github.com/LordMike/TMDbLib)
- [MudBlazor](http://mudblazor.com/)

