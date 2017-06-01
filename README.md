# .NET SDK

Version: profitbricks-sdk-net **3.0.4**

## Table of Contents

* [Description](#description)
* [Getting Started](#getting-started)
    * [Installation](#installation)
    * [Authenticating](#authenticating)
* [Reference](#reference)
    * [Data Centers](#data-centers)
      * [List Data Centers](#list-data-centers)
      * [Retrieve a Data Center](#retrieve-a-data-center)
      * [Create a Data Center](#create-a-data-center)
      * [Update a Data Center](#update-a-data-center)
      * [Delete a Data Center](#delete-a-data-center)
    * [Locations](#locations)
      * [List Locations](#list-locations)
      * [Get a Location](#get-a-location)
    * [Servers](#servers)
      * [List Servers](#list-servers)
      * [Retrieve a Server](#retrieve-a-server)
      * [Create a Server](#create-a-server)
      * [Update a Server](#update-a-server)
      * [Delete a Server](#delete-a-server)
      * [List Attached Volumes](#list-attached-volumes)
      * [Attach a Volume](#attach-a-volume)
      * [Retrieve an Attached Volume](#retrieve-an-attached-volume)
      * [Detach a Volume](#detach-a-volume)
      * [List Attached CD-ROMs](#list-attached-cd-roms)
      * [Attach a CD-ROM](#attach-a-cd-rom)
      * [Retrieve an Attached CD-ROM](#retrieve-an-attached-cd-rom)
      * [Detach a CD-ROM](#detach-a-cd-rom)
      * [Reboot a Server](#reboot-a-server)
      * [Start a Server](#start-a-server)
      * [Stop a Server](#stop-a-server)
    * [Images](#images)
      * [List Images](#list-images)
      * [Get an Image](#get-an-image)
      * [Update an Image](#update-an-image)
      * [Delete an Image](#delete-an-image)
    * [Volumes](#volumes)
      * [List Volumes](#list-volumes)
      * [Get a Volume](#get-a-volume)
      * [Create a Volume](#create-a-volume)
      * [Update a Volume](#update-a-volume)
      * [Delete a Volume](#delete-a-volume)
      * [Create a Volume Snapshot](#create-a-volume-snapshot)
      * [Restore a Volume Snapshot](#restore-a-volume-snapshot)
    * [Snapshots](#snapshots)
      * [List Snapshots](#list-snapshots)
      * [Get a Snapshot](#get-a-snapshot)
      * [Update a Snapshot](#update-a-snapshot)
      * [Delete a Snapshot](#delete-a-snapshot)
    * [IP Blocks](#ip-blocks)
    * [List IP Blocks](#list-ip-blocks)
      * [Get an IP Block](#get-an-ip-block)
      * [Create an IP Block](#create-an-ip-block)
      * [Delete an IP Block](#delete-an-ip-block)
    * [LANs](#lans)
      * [List LANs](#list-lans)
      * [Create a LAN](#create-a-lan)
      * [Get a LAN](#get-a-lan)
      * [Update a LAN](#update-a-lan)
      * [Delete a LAN](#delete-a-lan)
    * [Network Interfaces (NICs)](#network-interfaces)
      * [List NICs](#list-nics)
      * [Get a NIC](#get-a-nic)
      * [Create a NIC](#create-a-nic)
      * [Update a NIC](#update-a-nic)
      * [Delete a NIC](#delete-a-nic)
    * [Firewall Rules](#firewall-rules)
      * [List Firewall Rules](#list-firewall-rules)
      * [Get a Firewall Rule](#get-a-firewall-rule)
      * [Create a Firewall Rule](#create-a-firewall-rule)
      * [Update a Firewall Rule](#update-a-firewall-rule)
      * [Delete a Firewall Rule](#delete-a-firewall-rule)
    * [Load Balancers](#load-balancers)
      * [List Load Balancers](#list-load-balancers)
      * [Get a Load Balancer](#get-a-load-balancer)
      * [Create a Load Balancer](#create-a-load-balancer)
      * [Update a Load Balancer](#update-a-load-balancer)
      * [List Load Balanced NICs](#list-load-balanced-nics)
      * [Get a Load Balanced NIC](#get-a-load-balanced-nic)
      * [Associate NIC to a Load Balancer](#associate-nic-to-a-load-balancer)
      * [Remove a NIC Association](#remove-a-nic-association)
    * [Requests](#requests)
      * [List Requests](#list-requests)
      * [Get a Request](#get-a-request)
      * [Get a Request Status](#get-a-request-status)
* [Examples](#examples)
* [Support](#support)
* [Testing](#testing)
* [Contributing](#contributing)

## Description

This .NET library wraps the ProfitBricks Cloud API. All API operations are performed over a SSL/TLS secured connection and authenticated using your ProfitBricks portal credentials. The Cloud API can be accessed over the public Internet from any application that can send an HTTPS request and receive an HTTPS response.

This guide will show you how to programmatically perform common management tasks using the [.NET SDK](https://github.com/profitbricks/profitbricks-sdk-net) for the ProfitBricks Cloud API.

[.NET](http://www.microsoft.com/net) is a software framework developed by Microsoft that primarily runs on the Microsoft Windows operating system.

## Getting Started

Before you begin you will need to have [signed up](https://www.profitbricks.com/signup) for a ProfitBricks account. The credentials you setup during sign-up will be used to authenticate against the Cloud API.

### Installation

The official .NET library is available from the [ProfitBricks GitHub account](https://github.com/profitbricks/profitbricks-sdk-net). You can download the latest stable version by cloning the repository and then adding the project to your solution.

Or you can add the SDK by using `nuget`:

	Install-Package ProfitBricksSDK

### Authenticating

Connecting to ProfitBricks is handled by first setting up your authentication credentials.

To setup your credentials you will have to provide an instance of the Configuration class provided with your username and password

    public static Configuration Configuration
    {
        get
        {
            return new Configuration
            {
                Username = Environment.GetEnvironmentVariable("PROFITBRICKS_USERNAME"),
                Password = Environment.GetEnvironmentVariable("PROFITBRICKS_PASSWORD"),

            };
         }
    }

You can choose to read them from the environment variables as in the example above, or just provide the string value for `Username` and `Password`.

    var configuration = new Configuration
    {
        Username = "username@domain.tld",
        Password = "strong_pwd"
    };

You can now create an instance of any API class and pass the Configuration property for any future request.

	 LocationApi locApi = new LocationApi(Configuration);

## Reference

This section provides details on all the available operations and the arguments they accept. Brief code snippets demonstrating usage are also included.

### Data Centers

Virtual Data Centers (VDCs) are the foundation of the ProfitBricks platform. VDCs act as logical containers for all other objects you will be creating, e.g., servers. You can provision as many VDCs as you want. VDCs have their own private network and are logically segmented from each other to create isolation.

Create an instance of the API class:

    DataCenterApi dcApi = new DataCenterApi(Configuration);

#### List Data Centers

Lists all currently provisioned VDCs that are accessible for your account credentials.

The optional `depth` argument defines the level of information returned by the response. A depth of 1 returns the least amount of information; 5 returns the most.

```
var list = dcApi.FindAll(depth: 5);
```

---

#### Retrieve a Data Center

Retrieves details about a specific VDC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| depth | no | int | The level of details returned. |

    var dc = dcApi.FindById(DatacenterId, depth: 5);

---

#### Create a Data Center

Creates a new VDC. You can create a "simple" VDC by supplying just the required `name` and `location` arguments. This operation also has the capability of provisioning a "complex" VDC by supplying additional arguments for servers, volumes, LANs, and/or load balancers.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| datacenter | **yes** | object | An object containing the properties of the VDC you want to create. |

Declare a `Datacenter` object and assign the relevant properties:

    var datacenter = new Datacenter
    {
        Properties = new DatacenterProperties
        {
            Name = ".Net V2 - DC Test " + DateTime.Now.ToShortTimeString(),
            Description = "Unit test for .Net SDK PB REST V2",
            Location = "us/las"
            },
                Entities = new DatacenterEntities
                {
                    Servers = new Servers
                    {
                        Items = new List<Server>
                        {
                            new Server
                            {
                                Properties = new ServerProperties
                                {
                                    Name = "Test Server",
                                    Cores = 1,
                                    Ram = 1024
                                }
                            }
                        }
                    }
                }
    };

Call the `Create` method and pass in the `datacenter` object:

    datacenter = dcApi.Create(datacenter);

##### Data Center Object Reference

**Datacenter Object Properties**

| Name| Required | Type | Description |
|---|:-:|---|---|
| Name | **yes** | string | A name for the VDC. |
| Location | **yes** | string | The ProfitBricks regional location where the VDC will be created. |
| Description | no | string | A description for the VDC, e.g. staging, production. |

**NOTES**:
- The value for `Name` cannot contain the characters: (@, /, , |, ‘’, ‘).
- You cannot change the VDC `Location` once it has been provisioned.

**Supported Locations**

| Value| Country | City |
|---|---|---|
| us/las | United States | Las Vegas |
| us/ewr | United States | Newark |
| de/fra | Germany | Frankfurt |
| de/fkb | Germany | Karlsruhe |

These `DatacenterEntities` may also be supplied:

| Name| Required | Type | Description |
|---|:-:|---|---|
| Servers | no | Servers | An array containing one or more Server ojbects. See [create a server](#create-a-server). |
| Volumes | no | Volumes | An array containing one or more Volume objects. See [create a volume](#create-a-volume). |
| Lans | no | Lans | An array containing one or more Lan objects. See [create a lan](#create-a-lan). |
| Loadbalancers | no | Loadbalancers | An array containing one or more Loadbalancer objects. See [create a load balancer](#create-a-load- balancer). |

---

#### Update a Data Center

After retrieving a VDC, either by getting it by ID, or as a create response object, you can change its properties by calling the `PartialUpdate` or the `Update` method. Some values may not be changed using either of the update methods as they are read-only after the VDC is created.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| Name | no | string | The new name of the VDC. |
| Description | no | string | The new description of the VDC. |

    var resp = dcApi.PartialUpdate(DatacenterId, new DatacenterProperties { Name ="Test Update"});

**NOTE**: You may also use `Update()` instead of `PartialUpdate()`. For an `Update()` operation you will need to supply values for **all** the parameters.

---

#### Delete a Data Center

This will remove all objects within the VDC **AND** remove the VDC itself.

**NOTE**: This is a highly destructive operation which should be used with extreme caution.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC that you want to delete. |

    var resp = dcApi.Delete(DatacenterId);

---

### Locations

Locations are the physical ProfitBricks regional data centers where you can provision your VDCs.

Create an instance of the API class:

    LocationApi locApi = new LocationApi(Configuration);

#### List Locations

Returns the list of currently available locations.

The optional `depth` argument defines the level of information returned by the response. A depth of 1 returns the least amount of information; 5 returns the most.

    var locations = locApi.FindAll();

---

#### Get a Location

Retrieves the attributes of a specific location.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| LocationId | **yes** | string | The ID consisting of country/city. |

    Location loc = locApi.FindById(locationId);

---

### Servers

Creates an instance of these API classes:

    ServerApi serverApi = new ServerApi(Configuration);
    AttachedCDROMsApi attachCDROMApi = new AttachedCDROMsApi(Configuration);
    AttachedVolumesApi attachedVolumesApi = new AttachedVolumesApi(Configuration);

#### List Servers

Retrieves a list of all the servers provisioned inside a specific VDC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| depth | no | int | The level of details returned. |

    var list = serverApi.FindAll(DatacenterId);

---

#### Retrieve a Server

Returns information about a specific server such as its configuration, provisioning status, etc.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| depth | no | int | The level of details returned. |

    var server = serverApi.FindById(DatacenterId, ServerId);

---

#### Create a Server

Creates a server within an existing VDC. You can configure additional properties such as specifying a boot volume and connecting the server to a LAN.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| server | **yes** | Server | An object describing the server to be created. |

Declare a `Server` object and assign the relevant properties:

    var server = new Server
    {
        Properties = new ServerProperties
        {
            Name = "Test Server",
            Cores = 1,
            Ram = 1024,
        }
     };

Call the `Create` method and pass in the arguments including the `server` object:

    serverApi.Create(DatacenterId, server);

##### Server Object Reference

**Server Object Properties**

| Name| Required | Type | Description |
|---|:-:|---|---|
| Name | **yes** | string | The name of the server. |
| Cores | **yes** | int | The total number of cores for the server. |
| Ram | **yes** | int | The amount of memory for the server in MB, e.g. 2048. Size must be specified in multiples of 256 MB with a minimum of 256 MB; however, if you set `ram_hot_plug` to *True* then you must use a minimum of 1024 MB. |
| AvailabilityZone | no | string | The availability zone in which the server should exist. |
| CpuFamily | no | string | Sets the CPU type. "AMD_OPTERON" or "INTEL_XEON". Defaults to "AMD_OPTERON". |
| BootVolume | no | object | Reference to a volume used for booting. If not *null* then `BootCdrom` has to be *null*. |
| BootCdrom | no | object | Reference to a CD-ROM used for booting. If not *null* then `BootVolume` has to be *null*. |
| Volumes | no | object | A collection of volume objects that you want to create and attach to the server.|
| Nics | no | object | A collection of NICs you wish to create at the time the server is provisioned. |

**Supported Server (Compute) Availability Zones**

| Availability Zone | Comment |
|---|---|
| AUTO | Automatically Selected Zone |
| ZONE_1 | Fire Zone 1 |
| ZONE_2 | Fire Zone 2 |

---

#### Update a Server

Performs updates to the attributes of a server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| Name | no | string | The name of the server. |
| Cores | no | int | The number of cores for the server. |
| Ram | no | int | The amount of memory in the server. |
| AvailabilityZone | no | string | The new availability zone for the server. |
| CpuFamily | no | string | Sets the CPU type. "AMD_OPTERON" or "INTEL_XEON". Defaults to "AMD_OPTERON". |
| BootVolume | no | object | Reference to a volume used for booting. If not *null* then `BootCdrom` has to be *null* |
| BootCdrom | no | object | Reference to a CD-ROM used for booting. If not *null* then `BootVolume` has to be *null*. |

After retrieving a server, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var updated = serverApi.PartialUpdate(DatacenterId, ServerId, new ServerProperties { Name ="Updated" });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a Server

Removes a server from a VDC.

**Please Note**: This will not automatically remove the storage volume(s) attached to a server. A separate operation is required to delete a storage volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Delete` method directly:

    var response = serverApi.Delete(DatacenterId, ServerId);

---

#### List Attached Volumes

Retrieves a list of volumes attached to the server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| depth | no | int | The level of details returned. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `FindAll` method directly:

    var all = attachedVolumesApi.FindAll(DatacenterId, ServerId);

---

#### Attach a Volume

Attaches a pre-existing storage volume to the server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| VolumeId | **yes** | string | The ID of a storage volume. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `AttachVolume` method directly.

    var resp = attachedVolumesApi.AttachVolume(DatacenterId, ServerId, new Volume { Id = VolumeId });

---

#### Retrieve an Attached Volume

Retrieves the properties of an attached volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| VolumeId | **yes** | string | The ID of the attached volume. |
| depth | no | int | The level of details returned. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `FindById` method directly.

    var attachedVol = attachedVolumesApi.FindById(DatacenterId, ServerId, VolumeId);

---

#### Detach a Volume

Detaches the volume from the server. Depending on the volume's `hot_unplug` settings, this may result in the server being rebooted.

This will NOT delete the volume from your virtual data center. You will need to make a separate request to delete a volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| VolumeId | **yes** | string | The ID of the attached volume. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `DetachVolume` method directly.

    var resp = attachedVolumesApi.DetachVolume(DatacenterId,ServerId, VolumeId);

---

#### List Attached CD-ROMs

Retrieves a list of CD-ROMs attached to a server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| depth | no | int | The level of details returned. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `FindAll` method directly.

    var listAttached = attachCDROMApi.FindAll(DatacenterId, ServerId);

---

#### Attach a CD-ROM

Attaches a CD-ROM to an existing server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| CdRomImageId | **yes** | string | The ID of a CD-ROM. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Attach` method directly.

    var attached = attachCDROMApi.Attach(DatacenterId, ServerId, new Image { Id=CdRomImageId});

---

#### Retrieve an Attached CD-ROM

Retrieves a specific CD-ROM attached to the server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| CdRomImageId | **yes** | string | The ID of the attached CD-ROM. |
| depth | no | int | The level of details returned. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `FindById` method directly.

    var getAttached = attachCDROMApi.FindById(DatacenterId, ServerId, CdRomImageId);

---

#### Detach a CD-ROM

Detaches a CD-ROM from the server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| CdRomImageId | **yes** | string | The ID of the attached CD-ROM. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Detach` method directly.

    var removed = attachCDROMApi.Detach(DatacenterId, ServerId, CdRomImageId);

---

#### Reboot a Server

Forces a hard reboot of the server. Do not use this method if you want to gracefully reboot the machine. This is the equivalent of powering off the machine and turning it back on.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Reboot` method directly.

    var resp = serverApi.Reboot(DatacenterId, ServerId);

---

#### Start a Server

Starts a server. If the server's public IP address was deallocated, a new IP address will be assigned.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Start` method directly.

    var start = serverApi.Start(DatacenterId, ServerId);

---

#### Stop a Server

Stops a server. The machine will be forcefully powered off, billing will stop, and the public IP address, if one is allocated, will be deallocated.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |

After retrieving a server, either by getting it by ID, or as a create response object, you can call the `Stop` method directly.

    var error = serverApi.Stop(DatacenterId, ServerId);

---

### Images

Creates an instance of the API class:

    ImageApi imageApi = new ImageApi(Configuration);

#### List Images

Retrieves a list of images.

The optional `depth` argument defines the level of information returned by the response. A depth of 1 returns the least amount of information; 5 returns the most.

    var list = imageApi.FindAll();

---

#### Get an Image

Retrieves the attributes of a specific image.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| ImageId | **yes** | string | The ID of the image. |
| depth | no | int | The level of details returned. |

    var img = imageApi.FindById(ImageId);

---

### Volumes

Creates an instance of the API class:

    AttachedVolumesApi attachedVolumesApi = new AttachedVolumesApi(Configuration);

#### List Volumes

Retrieves a list of volumes within the virtual data center. If you want to retrieve a list of volumes attached to a server please see the [List Attached Volumes](#list-attached-volumes) entry in the Server section for details.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| depth | no | int | The level of details returned. |

    var list = volumeApi.FindAll(DatacenterId);

---

#### Get a Volume

Retrieves the attributes of a given volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| VolumeId | **yes** | string | The ID of the volume. |
| depth | no | int | The level of details returned. |

    var volume = volumeApi.FindById(DatacenterId, VolumeId);

---

#### Create a Volume

Creates a volume within the VDC. This will NOT attach the volume to a server. Please see the [Attach a Volume](#attach-a-volume) entry in the Server section for details on how to attach storage volumes.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| volume | **yes** | Volume | A Volume object. See [Volume Object Resource](#volume-object-resource). |

Declare a `Volume` object and assign the relevant properties:

    var volume = new Volume
    {
        Properties = new VolumeProperties
        {
            Size = 40,
            LicenceType = "OTHER",
            Type = "HDD",
            Name = ".Net V2 - Test " + DateTime.Now.ToShortTimeString(),
            AvailabilityZone = "ZONE_3"
        }
    };

Call the `Create` method and pass in the arguments including the `volume` object:

    volume = volumeApi.Create(DatacenterId, volume);

##### Volume Object Reference

**Volume Object Properties**

| Name| Required | Type | Description |
|---|:-:|---|---|
| Size | **yes** | int | The size of the volume in GB. |
| Image | **yes** | string | The image or snapshot ID. |
| Type | **yes** | string | The volume type, HDD or SSD. |
| LicenceType | **yes** | string | The licence type of the volume. Options: LINUX, WINDOWS, WINDOWS2016, UNKNOWN, OTHER |
| ImagePassword | **yes** | string | One-time password is set on the Image for the appropriate root or administrative account. This field may only be set in creation requests. When reading, it always returns *null*. The password has to contain 8-50 characters. Only these characters are allowed: [abcdefghjkmnpqrstuvxABCDEFGHJKLMNPQRSTUVX23456789] |
| SshKeys | **yes** | string | SSH keys to allow access to the volume via SSH. |
| Name | no | string | The name of the volume. |
| Bus | no | string | The bus type of the volume (VIRTIO or IDE). Default: VIRTIO. |
| AvailabilityZone | no | string | The storage availability zone assigned to the volume. Valid values: AUTO, ZONE_1, ZONE_2, or ZONE_3. This only applies to HDD volumes. Leave blank or set to AUTO when provisioning SSD volumes. |

**Licence Types**

| Licence Type | Comment |
|---|---|
| WINDOWS2016 | Use this for the Microsoft Windows Server 2016 operating system. |
| WINDOWS | Use this for the Microsoft Windows Server 2008 and 2012 operating systems. |
| LINUX |Use this for Linux distributions such as CentOS, Ubuntu, Debian, etc. |
| OTHER | Use this for any volumes that do not match one of the other licence types. |
| UNKNOWN | This value may be inherited when you've uploaded an image and haven't set the license type. Use one of the options above instead. |

**Supported Volume (Storage) Availability Zones**

| Availability Zone | Comment |
|---|---|
| AUTO | Automatically Selected Zone |
| ZONE_1 | Fire Zone 1 |
| ZONE_2 | Fire Zone 2 |
| ZONE_3 | Fire Zone 3 |

**Please Note:** You will need to provide either the `Image` or the `LicenceType` parameters. `LicenceType` is required, but if `Image` is supplied, it is already set and cannot be changed. Similarly either the `ImagePassword` or `SshKeys` parameters need to be supplied when creating a volume.

---

#### Update a Volume

Various attributes on the volume can be updated (either in full or partially) although the following restrictions apply: 

* The size of an existing storage volume can be increased. It cannot be reduced.
* The volume size will be increased without requiring a reboot if the relevant hot plug settings have been set to `true`. 
* The additional capacity is not added automatically added to any partition, therefore you will need to handle that inside the OS afterwards. 
* After you have increased the volume size you cannot decrease the volume size.

Since an existing volume is being modified, none of the request arguments are specifically required as long as the changes being made satisfy the requirements for creating a volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| VolumeId | **yes** | string | The ID of the volume. |
| Name | no | string | The name of the volume. |
| Size | no | int | The size of the volume in GB. Only increase when updating. |
| Bus | no | string | The bus type of the volume (VIRTIO or IDE). Default: VIRTIO. |
| Image | no | string | The image or snapshot ID. |
| Type | no | string | The volume type, HDD or SSD. |
| LicenceType | no | string | The licence type of the volume. Options: LINUX, WINDOWS, WINDOWS2016, UNKNOWN, OTHER |
| AvailabilityZone | no | string | The storage availability zone assigned to the volume. Valid values: AUTO, ZONE_1, ZONE_2, or ZONE_3. This only applies to HDD volumes. Leave blank or set to AUTO when provisioning SSD volumes. |

After retrieving a volume, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var newVolume = volumeApi.PartialUpdate(DatacenterId, VolumeId, new VolumeProperties { Size = volume.Properties.Size + 1 });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a Volume

Deletes the specified volume. This will result in the volume being removed from your VDC. Use this with caution.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| VolumeId | **yes** | string | The ID of the volume. |

After retrieving a volume, either by getting it by id, or as a create response object, you can call the `Delete` method directly.

    var response = volumeApi.Delete(DatacenterId, VolumeId);

---

#### Create a Volume Snapshot

Creates a snapshot of a volume within the virtual data center. You can use a snapshot to create a new storage volume or to restore a storage volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| VolumeId | **yes** | string | The ID of the volume. |
| Name | no | string | The name of the snapshot. |
| Description | no | string | The description of the snapshot. |

After retrieving a volume, either by getting it by ID, or as a create response object, you can call the `CreateSnapshot` method directly.

    var resp = volumeApi.CreateSnapshot(DatacenterId, VolumeId, Name, Description);

---

#### Restore a Volume Snapshot

Restores a snapshot onto a volume. A snapshot is created as just another image that can be used to create new volumes or to restore an existing volume.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| SnapshotId | **yes** | string |  The ID of the snapshot. |

After retrieving a volume, either by getting it by id, or as a create response object, you can call the `RestoreSnapshot` method directly.

    var resp = volumeApi.RestoreSnapshot(DatacenterId, SnapshotId);

---

### Snapshots

Creates an instance of the API class:

    SnapshotApi snapshotApi = new SnapshotApi(Configuration);

#### List Snapshots

Retrieves a list of all available snapshots.

The optional `depth` argument defines the level of information returned by the response. A depth of 1 returns the least amount of information; 5 returns the most.

    var list = snapshotApi.FindAll(depth: 5);

---

#### Get a Snapshot

Retrieves the attributes of a specific snapshot.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| SnapshotId | **yes** | string | The ID of the snapshot. |
| depth | no | int | The level of details returned. |

    var snapshot = snapshotApi.FindById(SnapshotId);

---

#### Update a Snapshot

Performs updates to attributes of a snapshot.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| SnapshotId | **yes** | string | The ID of the snapshot. |
| Name | no | string | The name of the snapshot. |
| Description | no | string | The description of the snapshot. |
| LicenceType | no | string | The snapshot's licence type: LINUX, WINDOWS, WINDOWS2016, or OTHER. |
| CpuHotPlug | no | bool | This volume is capable of CPU hot plug (no reboot required) |
| CpuHotUnplug | no | bool | This volume is capable of CPU hot unplug (no reboot required) |
| RamHotPlug | no | bool |  This volume is capable of memory hot plug (no reboot required) |
| RamHotUnplug | no | bool | This volume is capable of memory hot unplug (no reboot required) |
| NicHotPlug | no | bool | This volume is capable of NIC hot plug (no reboot required) |
| NicHotUnplug | no | bool | This volume is capable of NIC hot unplug (no reboot required) |
| DiscVirtioHotPlug | no | bool | This volume is capable of VirtIO drive hot plug (no reboot required) |
| DiscVirtioHotUnplug | no | bool | This volume is capable of VirtIO drive hot unplug (no reboot required) |
| DiscScsiHotPlug | no | bool | This volume is capable of SCSI drive hot plug (no reboot required) |
| DiscScsiHotUnplug | no | bool | This volume is capable of SCSI drive hot unplug (no reboot required) |

After retrieving a snapshot, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var updated = snapshotApi.PartialUpdate(SnapshotId, new SnapshotProperties { Name ="Updated" });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a Snapshot

Deletes the specified snapshot.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| SnapshotId | **yes** | string | The ID of the snapshot. |

After retrieving a snapshot, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    var resp = snapshotApi.Delete(SnapshotId);

---

### IP Blocks

The IP block operations assist with managing reserved static public IP addresses.

Create an instance of the API class:

    IPBlocksApi ipApi = new IPBlocksApi(Configuration);

#### List IP Blocks

Retrieves a list of available IP address blocks.

The optional `depth` argument defines the level of information returned by the response. A depth of 1 returns the least amount of information; 5 returns the most.

    var list = ipApi.FindAll();

#### Get an IP Block

Retrieves the attributes of a specific IP address block.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| IpBlockId | **yes** | string | The ID of the IP address block. |
| depth | no | int | The level of details returned. |

    var ip = ipApi.FindById(IpBlockId);

---

#### Create an IP Block

Creates an IP address block. IP blocks are attached to a location, so you must specify a valid `location` along with a `size` argument indicating the number of IP addresses you want to reserve in the IP block. Servers or other resources using an IP address from an IP block must be in the same `location`.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| ipBlock | **yes** | IpBlock | [IpBlock object](#ipblock-object-resource). |

Declare an `IpBlock` object and assign the relevant properties:

    var ipBlock = new IpBlock
    {
        Properties = new IpBlockProperties
        {
            Location = "us/las",
            Size = 1
        }
    };

Call the `Create` method and pass in the `ipBlock` object:

    ipBlock = ipApi.Create(ipBlock);

##### IpBlock Object Reference

| Name| Required | Type | Description |
|---|:-:|---|---|
| Location | **yes** | string | This must be one of the locations: us/las, us/ewr, de/fra, de/fkb. |
| Size | **yes** | int | The size of the IP block you want. |
| Name | no | string | A descriptive name for the IP block |

**Supported Locations**

| Value| Country | City |
|---|---|---|
| us/las | United States | Las Vegas |
| us/ewr | United States | Newark |
| de/fra | Germany | Frankfurt |
| de/fkb | Germany | Karlsruhe |

---

#### Delete an IP Block

Deletes the specified IP Block.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| IpBlockId | **yes** | string | The ID of the IP block. |

After retrieving an IP block, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    var resp = ipApi.Delete(IpBlockId);

---

### LANs

#### List LANs

Retrieves a list of LANs within the VDC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| depth | no | int | The level of details returned. |

    var list = lanApi.FindAll(DatacenterId);

---

#### Create a LAN

Creates a LAN within a VDC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| lan | **yes** | Lan | Lan object. |

Declare a `Lan` object and assign the relevant properties:

    var lan = lanApi.Create(DatacenterId, new Lan
    {
        Properties = new LanProperties
        {
            Public = true,
            Name = ".Net V2 - Test " + DateTime.Now.ToShortTimeString()
        },
        Entities=new LanEntities
        {
            Nics= new LanNics
            {
                Items= new List<Nic>()
                {
                    new Nic
                    {
                        Id=nic.Id
                    }
                }
            }
        }
    };

Call the `Create` method and pass in the arguments including the `lan` object:

##### Lan Object Reference

| Name| Required | Type | Description |
|---|:-:|---|---|
| Public | **Yes** | bool | Boolean indicating if the LAN faces the public Internet or not. |
| Name | no | string | The name of your LAN. |
| Nics | no | object | A collection of NICs associated with the LAN. |

---

#### Get a LAN

Retrieves the attributes of a given LAN.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LanId | **yes** | int | The ID of the LAN. |
| depth | no | int | The level of details returned. |

    var lan = lanApi.FindById(DatacenterId, LanId);

---

#### Update a LAN

Performs updates to attributes of a LAN.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LanId | **yes** | int | The ID of the LAN. |
| Name | no | string | A descriptive name for the LAN. |
| Public | no | bool | Boolean indicating if the LAN faces the public Internet or not. |

After retrieving a LAN, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var updated = lanApi.PartialUpdate(DatacenterId, LanId, new LanProperties { Public = True });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a LAN

Deletes the specified LAN.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LanId | **yes** | string | The ID of the LAN. |

After retrieving a LAN, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    lanApi.Delete(DatacenterId,LanId);

---

### Network Interfaces

Creates an instance of the API class:

    NetworkInterfacesApi nicApi = new NetworkInterfacesApi(Configuration);

#### List NICs

Retrieves a list of LANs within the virtual data center.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| depth | no | int | The level of details returned. |

    var list = nicApi.FindAll(DatacenterId, ServerId);

---

#### Get a NIC

Retrieves the attributes of a given NIC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| depth | no | int | The level of details returned. |

    var nic = nicApi.FindById(DatacenterId, ServerId, NicId);

---

#### Create a NIC

Adds a NIC to the target server.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string| The ID of the server. |
| nic | **yes** | Nic | Nic object. |

Declare a `Nic` object and assign the relevant properties:

    var nic = new Nic
    {
        Properties = new NicProperties
        {
            Lan = 1,
            Nat = false
        }
    };

Call the `Create` method and pass in the arguments including the `nic` object:

    nic = nicApi.Create(DatacenterId, ServerId, nic);

##### Nic Object Reference

| Name| Required | Type | Description |
|---|:-:|---|---|
| Lan | **yes** | int | The LAN ID the NIC will sit on. If the LAN ID does not exist it will be created. |
| Name | no | string | The name of the NIC. |
| Ips | no | string collection | IP addresses assigned to the NIC. This can be a collection. |
| Dhcp | no | bool | Set to FALSE if you wish to disable DHCP on the NIC. Default: TRUE. |
| Nat | no | bool | Indicates the private IP address has outbound access to the public internet. |
| FirewallActive | no | bool | Once you add a firewall rule this will reflect a true value. |
| FirewallRules | no | object| A list of firewall rules associated to the NIC represented as a collection. |

---

#### Update a NIC

Various attributes on the volume can be updated (either in full or partially) although the following restrictions apply: 

* The primary address of a NIC connected to a load balancer can only be changed by changing the IP address of the load balancer. 
* You can also add additional reserved, public IP addresses to the NIC.

The user can specify and assign private IP addresses manually. Valid IP addresses for private networks are 10.0.0.0/8, 172.16.0.0/12 or 192.168.0.0/16.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string| The ID of the server. |
| NicId | **yes** | string| The ID of the NIC. |
| Name | no | string | The name of the NIC. |
| Ips | no | string collection | IP addresses assigned to the NIC represented as a collection. |
| Dhcp | no | bool | Boolean value that indicates if the NIC is using DHCP or not. |
| Lan | no | int | The LAN ID the NIC sits on. |
| Nat | no | bool | Indicates the private IP address has outbound access to the public internet. |

After retrieving a NIC, either by getting it by ID, or as a create response object, you can call the `PartialUpdate` method directly.

    var updated = nicApi.PartialUpdate(DatacenterId, ServerId, NicId, new NicProperties { Name ="Update", Ips = new System.Collections.Generic.List<string> { "10.8.52.225", "1.1.1.1" } });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a NIC

Deletes the specified NIC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string| The ID of the server. |
| NicId | **yes** | string| The ID of the NIC. |

After retrieving a NIC, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    var resp = nicApi.Delete(DatacenterId, ServerId, NicId);

---

### Firewall Rules

Creates an instance of the API class:

    FirewallRuleApi fwApi = new FirewallRuleApi(Configuration);

#### List Firewall Rules

Retrieves a list of firewall rules associated with a particular NIC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| depth | no | int | The level of details returned. |

    var list = fwApi.FindAll(DatacenterId, ServerId, NicId);

---

#### Get a Firewall Rule

Retrieves the attributes of a given firewall rule.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| FirewallRuleId | **yes** | string | The ID of the firewall rule. |
| depth | no | int | The level of details returned. |

    var newFw = fwApi.FindById(DatacenterId, ServerId, NicId, FirewallRuleId);

---

#### Create a Firewall Rule

Adds a firewall rule to the NIC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| fw | **yes** | object | |

Declare a `FirewallRule` object and assign the relevant properties:

    var fw = new FirewallRule { Properties = new FirewallruleProperties { Protocol = "TCP", Name = ".Net" } };

Call the `Create` method and pass in the arguments including the `fw` object:

    fw = fwApi.Create(DatacenterId, ServerId, NicId, fw);

##### Firewall Object Reference

| Name| Required | Type | Description |
|---|:-:|---|---|
| Protocol | **yes** | string | The protocol for the rule: TCP, UDP, ICMP, ANY. |
| Name | no | string | The name of the firewall rule. |
| SourceMac | no | string | Only traffic originating from the respective MAC address is allowed. Valid format: aa:bb:cc:dd:ee:ff. A *null* value allows all source MAC address. |
| SourceIp | no | string | Only traffic originating from the respective IPv4 address is allowed. A *null* value allows all source IP addresses. |
| TargetIp | no | string | In case the target NIC has multiple IP addresses, only traffic directed to the respective IP address of the NIC is allowed. A *null* value allows all target IP addresses. |
| PortRangeStart | no | string | Defines the start range of the allowed port (from 1 to 65534) if protocol TCP or UDP is chosen. Leave `PortRangeStart` and `PortRangeEnd` value as *null* to allow all ports. |
| PortRangeEnd | no | string | Defines the end range of the allowed port (from 1 to 65534) if the protocol TCP or UDP is chosen. Leave `PortRangeStart` and `PortRangeEnd` value as *null* to allow all ports. |
| IcmpType | no | string | Defines the allowed type (from 0 to 254) if the protocol ICMP is chosen. A *null* value allows all types. |
| IcmpCode | no | string | Defines the allowed code (from 0 to 254) if protocol ICMP is chosen. A *null* value allows all codes. |

---

#### Update a Firewall Rule

Performs updates to attributes of a firewall rule.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| FirewallRuleId | **yes** | string | The ID of the firewall rule. |
| Name | no | string | The name of the firewall rule. |
| SourceMac | no | string | Only traffic originating from the respective MAC address is allowed. Valid format: aa:bb:cc:dd:ee:ff. A *null* value allows all source MAC address. |
| SourceIp | no | string | Only traffic originating from the respective IPv4 address is allowed. A *null* value allows all source IP addresses. |
| TargetIp | no | string | In case the target NIC has multiple IP addresses, only traffic directed to the respective IP address of the NIC is allowed. A *null* value allows all target IP addresses. |
| PortRangeStart | no | string | Defines the start range of the allowed port (from 1 to 65534) if protocol TCP or UDP is chosen. Leave `port_range_start` and `port_range_end` value as *null* to allow all ports. |
| PortRangeEnd | no | string | Defines the end range of the allowed port (from 1 to 65534) if the protocol TCP or UDP is chosen. Leave `port_range_start` and `port_range_end` value as *null* to allow all ports. |
| IcmpType | no | string | Defines the allowed type (from 0 to 254) if the protocol ICMP is chosen. A *null* value allows all types. |
| IcmpCode | no | string | Defines the allowed code (from 0 to 254) if protocol ICMP is chosen. A *null* value allows all codes. |

After retrieving a firewall rule, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var updated = fwApi.PartialUpdate(DatacenterId, ServerId, NicId, FirewallRuleId, new FirewallruleProperties {Name = "Updated" });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a Firewall Rule

Removes the specific firewall rule.

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| ServerId | **yes** | string | The ID of the server. |
| NicId | **yes** | string | The ID of the NIC. |
| FirewallRuleId | **yes** | string | The ID of the firewall rule. |

After retrieving a firewall rule, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    var resp = fwApi.Delete(DatacenterId, ServerId, NicId, FirewallRuleId);

---

### Load Balancers

Creates an instance of the API class:

    LoadBalancerApi lbApi = new LoadBalancerApi(Configuration)

#### List Load Balancers

Retrieves a list of load balancers within the data center.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| depth | no | int | The level of details returned. |

    var list = lbApi.FindAll(DatacenterId);

---

#### Get a Load Balancer

Retrieves the attributes of a given load balancer.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| depth | no | int | The level of details returned. |

    var lb = lbApi.FindById(DatacenterId, LoadBalancerId);

---

#### Create a Load Balancer

Creates a load balancer within the virtual data center. Load balancers can be used for traffic on either public or private IP addresses.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| lb | **yes** | Loadbalancer | LoadBalancer object. |

Declare a `Loadbalancer` object and assign the relevant properties:

    var lb = new Loadbalancer
    {
        Properties = new LoadbalancerProperties
        {
            Name = ".Net V2 - Test " + DateTime.Now.ToShortTimeString()
        }
    };

Call the `Create` method and pass in the arguments including the `lb` object:

    lb = lbApi.Create(DatacenterId, lb);

##### Loadbalancer Object Resource

| Name| Required | Type | Description |
|---|:-:|---|---|
| Name | **yes** | string | The name of the load balancer. |
| Ip | no | string | IPv4 address of the load balancer. All attached NICs will inherit this IP address. |
| Dhcp | no | bool | Indicates if the load balancer will reserve an IP address using DHCP. |
| BalancedNics | no | string collection | List of NICs taking part in load-balancing. All balanced NICs inherit the IP address of the load balancer. |

---

#### Update a Load Balancer

Performs updates to attributes of a load balancer.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| Name | no | string | The name of the load balancer. |
| Ip | no | string | The IP address of the load balancer. |
| Dhcp | no | bool | Indicates if the load balancer will reserve an IP address using DHCP. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can change its properties and call the `PartialUpdate` method:

    var newLb = lbApi.PartialUpdate(DatacenterId, LoadBalancerId, new LoadbalancerProperties { Name = "Updated" });

**NOTE**: You can also use `Update()`, for that operation you will update all the properties.

---

#### Delete a Load Balancer

Deletes the specified load balancer.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can call the `Delete` method directly.

    var resp = lbApi.Delete(DatacenterId, LoadBalancerId);

---

#### List Load Balanced NICs

Retrieves a list of NICs associated with the load balancer.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| depth | no | int | The level of details returned. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can call the `FindAll` method directly.

```
 var BalancedNics = lbApi.FindAll(DatacenterId, LoadBalancerId, depth);
```

---

#### Get a Load Balanced NIC

Retrieves the attributes of a given load balanced NIC.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| NicId | **yes** | string | The ID of the NIC. |
| depth | no | int | The level of details returned. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can call the `get_loadbalanced_nic` method directly.

```
var balancedNic = lbApi.FindById(DatacenterId, LoadBalancerId, NicId, depth);
```

---

#### Associate NIC to a Load Balancer

Associates a NIC to a load balancer, enabling the NIC to participate in load-balancing.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| NicId | **yes** | string | The ID of the NIC. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can call the `AttachNic` method directly.

    NetworkInterfacesApi nicApi = new NetworkInterfacesApi(Configuration);
    var attachedNic = nicApi.AttachNic(DatacenterId, LoadBalancerId, new Nic { Id = NicId });

---

#### Remove a NIC Association

Removes the association of a NIC with a load balancer.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| DatacenterId | **yes** | string | The ID of the VDC. |
| LoadBalancerId | **yes** | string | The ID of the load balancer. |
| NicId | **yes** | string | The ID of the NIC. |

After retrieving a load balancer, either by getting it by ID, or as a create response object, you can call the `DetachNic` method directly.

    var resp = nicApi.DetachNic(DatacenterId, LoadBalancerId, NicId);

---

### Requests

Each call to the ProfitBricks Cloud API is assigned a request ID. These operations can be used to get information about the requests that have been submitted and their current status.

Create an instance of the API class:

    RequestApi reqApi = new RequestApi(Configuration);

#### List Requests

Retrieves a list of requests.

    var requests = reqApi.List();

---

#### Get a Request

Retrieves the attributes of a specific request.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| RequestId | **yes** | string | The ID of the request. |

    var req = reqApi.Get(RequestId);

---

#### Get a Request Status

Retrieves the status of a request.

**Request Arguments**

| Name| Required | Type | Description |
|---|:-:|---|---|
| RequestId | **yes** | string | The ID of the request. |

    RequestStatus req = reqApi.GetStatus(RequestId);

---

## Examples

Here are a few examples on how to use the module.

The next few sections describe some commonly used features of this .NET library and provide implementation examples.

### Create Virtual Data Center

ProfitBricks utilizes the concept of virtual data centers. These are logically separated from one and the other and allow you to have a self-contained environment for all servers, volumes, networking, snapshots, and so forth. The goal is to replicate the experience of managing resources in a physical data center.

You are required to have a virtual data center created before you can create any other objects. Think of the virtual data center as a bucket in which all objects, such as servers and volumes, live.

The following code example shows how to programmatically create a data center:


    namespace ProfitbricksV2.Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                var configuration = new Configuration
                {
                    Username = "username",
                    Password = "password",

                };
                var dcApi = new DataCenterApi(configuration);



                // CreateDataCenterRequest.
                // The only required field is DataCenterName.
                // If the location argument is left empty the virtual data center will be created in the user's default region
                var datacenter = new Datacenter
                {
                    Properties = new DatacenterProperties
                    {
                        Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
                        Description = "Test for dotNet SDK",
                        Location = "us/las"
                    }
                };


                datacenter = dcApi.Create(datacenter);
            }
        }
    }

### Delete Virtual Data Center

Use caution when deleting a data center. Deleting a data center will **destroy** all objects contained within that data center -- including all servers, volumes, snapshots, etc. **When the objects are deleted** they **cannot be recovered**.

This example deletes the virtual data center created above:

    dcApi.Delete(datacenter.Id);

### Create Server

This example creates a server and assigns it a number of processing cores and memory. This example also shows specifying the `CpuFamily` as "INTEL_XEON" rather than the default of "AMD_OPTERON" You may wish to refer to the Cloud API documentation to see the complete list of attributes available.

    var server = new Server
    {
    Properties = new ServerProperties
    {
        Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
        Cores = 1,
        Ram = 256,
        CpuFamily = "INTEL_XEON"
    }
    };

    // response will contain Id of a newly created server.
    server = serverApi.Create(datacenter.Id, server);

One of the unique features of the ProfitBricks platform is that it allows you to define your own settings for cores, memory, and disk size without being tied to a particular instance size.  

### Update Cores, Memory, and Disk

ProfitBricks allows users to dynamically update cores and memory independently of each other. This means that you do not have to upgrade to the larger size in order to increase memory. You can simply increase the instance's memory, which keeps your costs in line with your resource needs. 

This example updates cores and memory:

    server = serverApi.PartialUpdate(datacenter.Id, server.Id, new ServerProperties { Name = server.Properties.Name + " -Updated" });

### Detach and Reattach a Storage Volume

ProfitBricks allows for the creation of multiple storage volumes. You can attach and detach these on the fly. This is helpful in scenarios such as attaching a failed OS disk to another server for recovery, or moving a volume to another server to bring online.

This example creates a new 40 GB volume:

    //First we need to create a volume.
    var volume = new Volume
    {
        Properties = new VolumeProperties
        {
            Size = 40,
            Image = "fbaae2b2-c899-11e5-aa10-52540005ab80",
            Type = "HDD",
            Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
            ImagePassword = "r@nd0m_S3cure%str1nG"
            SshKeys = new System.Collections.Generic.List<string> { "hQGOEJeFL91EG3+l9TtRbWNjzhDVHeLuL3NWee6bekA=" }
        }
    };

    volume = volumeApi.Create(datacenter.Id, volume);

**Notes:**

* The value supplied for `Image` is an example and is unlikely to be valid. 
* ProfitBricks supplies a number of different images for various operating systems. These images are updated from time to time and these updates often result in a new UUID being issued. 
* The .NET SDK can help you to locate valid image IDs. There are also [CLI](https://devops.profitbricks.com/tools/cli) and [PowerShell](https://devops.profitbricks.com/tools/powershell) tools available on [DevOps Central](https://devops.profitbricks.com/tools/) which can help you find the currently available UUIDs.
* The value supplied for `Type` can be set to "HDD" or "SSD", depending on what storage type you want to use.
* Either `ImagePassword` or `SshKeys` needs to be set for volumes. It is possible to set both, however, `SshKeys` are only applicable to Linux images. Therefore you **MUST** provide an `ImagePassword` when creating volumes based on a Microsoft Windows Image.

Attach the volume to a server identified by `server.Id`:

    //Then we are going to attach the newly volume to a server.
    attachedVolumesApi.AttachVolume(datacenter.Id, server.Id, new Volume { Id = volume.Id });

Detach the volume from a server identified by `server.Id`:

    //Here we are going to detach it from the server.
    attachedVolumesApi.DetachVolume(datacenter.Id, server.Id, volume.Id);

### List Servers, Volumes, and Virtual Data Centers

You can pull various resource lists from your virtual data centers using the .NET library. The three most commonly queried resources are virtual data centers, servers, and volumes.

Examples of how to pull these three list types:

    var dcs = dcApi.FindAll(depth: 5);
    var servers = serverApi.FindAll(datacenter.Id, depth: 5);
    var volumes = volumeApi.FindAll(datacenter.Id, depth: 5);

### Create Additional Network Interfaces

The ProfitBricks cloud platform supports adding multiple NICs to a server. These NICs can be used to create different, segmented networks on the platform.

This example adds a second NIC to an existing server:

    var nic = new Nic { Properties = new NicProperties { Lan = 1 , Dhcp = true, Name = "Nic name"} };

    nic = nicApi.Create(datacenter.Id, server.Id, nic);

**Please Note:** Using this function will result in a running server being rebooted.

### Example

    using Api;
    using Client;
    using Model;
    using System;

    namespace ProfitbricksV2.Example
    {
        class Program
        {
            static void Main(string[] args)
            {
                var configuration = new Configuration
                {
                    Username = "username",
                    Password = "password",

                };
                var dcApi = new DataCenterApi(configuration);
                var serverApi = new ServerApi(configuration);
                var volumeApi = new VolumeApi(configuration);
                var attachedVolumesApi = new AttachedVolumesApi(configuration);
                var nicApi = new NetworkInterfacesApi(configuration);


                // CreateDataCenterRequest.
                // The only required field is DataCenterName.
                // If location the argument is left empty data center will be created in the default region of the customer
                var datacenter = new Datacenter
                {
                    Properties = new DatacenterProperties
                    {
                        Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
                        Description = "Test for dotNet SDK",
                        Location = "us/las"
                    }
                };

                // Response will contain Id of a newly created virtual data center.
                datacenter = dcApi.Create(datacenter);

                // CreateServer.
                // DatacenterId: Defines the virtual data center wherein the server is to be created.
                // AvailabilityZone: Selects the zone in which the server is going to be created (AUTO, ZONE_1, ZONE_2).
                // Cores: Number of cores to be assigned to the specified server. Required field.
                // InternetAccess: Set to TRUE to connect the server to the Internet via the specified LAN ID.
                // OsType: Sets the OS type of the server.
                // Ram: Amount of RAM memory (in MiB) to be assigned to the server.
                var server = new Server
                {
                    Properties = new ServerProperties
                    {
                        Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
                        Cores = 1,
                        Ram = 256
                    }
                };

                // response will contain the Id of a newly created server.
                server = serverApi.Create(datacenter.Id, server);

                // UpdateServer
                // ServerId: Id of the server to be updated.
                // ServerName: Renames target virtual server
                // Cores: Updates the amount of cores of the target virtual server
                // Ram: Updates the RAM memory (in MiB) of the target virtual server. The minimum RAM size is 256 MiB
                server = serverApi.PartialUpdate(datacenter.Id, server.Id, new ServerProperties { Name = server.Properties.Name + " -Updated" });


                // CreateVolume
                // DatacenterId: Defines the virtual data center wherein the storage is to be created. If left empty, the storage will be created in a new virtual data center
                // Size: Storage size (in GiB). Required Field.
                // Type: SSD or HDD disk type, Required Field
                // SshKeys: SSH keys, Optional field
                var volume = new Volume
                {
                    Properties = new VolumeProperties
                    {
                        Size = 4,
                        Image = "fbaae2b2-c899-11e5-aa10-52540005ab80",
                        Type = "HDD",
                        Name = "dotNet Test " + DateTime.Now.ToShortTimeString(),
                        SshKeys = new System.Collections.Generic.List<string> { "hQGOEJeFL91EG3+l9TtRbWNjzhDVHeLuL3NWee6bekA=" }
                    }
                };

                // Response will contain Id of a newly created volume.
                volume = volumeApi.Create(datacenter.Id, volume);

                // AttachVolume
                // ServerId: Identifier of the target virtual storage. Required field.
                // StorageId: Identifier of the virtual storage to be connected. Required field.
                // BusType: Bus type to which the storage will be connected
                attachedVolumesApi.AttachVolume(datacenter.Id, server.Id, new Volume { Id = volume.Id });

                attachedVolumesApi.DetachVolume(datacenter.Id, server.Id, volume.Id);

                // Fetches list of all Data Centers
                var dcs = dcApi.FindAll(depth: 5);

                // Fetches list of all Servers
                var servers = serverApi.FindAll(datacenter.Id, depth: 5);

                // Fetches list of all Volumes
                var volumes = volumeApi.FindAll(datacenter.Id, depth: 5);

                // CreateNicRequest
                // Identifier of the target virtual server. Required field.
                // Nic: Names the NIC
                // Toggles usage of ProfitBricks DHCP
                // Lan
                var nic = new Nic { Properties = new NicProperties { Lan = 1 , Dhcp = true, Name = "Nic name"} };

                nic = nicApi.Create(datacenter.Id, server.Id, nic);

            }
        }
    }

## Support

You can engage with us on the ProfitBricks [DevOps Central community](https://devops.profitbricks.com/community) site where we will be happy to answer any questions you might have about using this .NET library.

Please report any issues or bugs your encounter using the [GitHub Issue Tracker](https://github.com/profitbricks/profitbricks-sdk-net/issues).

## Testing

You can find a full list of tests inside the `ProfitbricksV2.Tests` project. You can run tests from the Visual Studio Test Explorer.

## Contributing

1. Fork it ( https://github.com/profitbricks/profitbricks-sdk-net/fork )
2. Create your feature branch (`git checkout -b my-new-feature`)
3. Commit your changes (`git commit -am 'Add some feature'`)
4. Push to the branch (`git push origin my-new-feature`)
5. Create a new Pull Request
