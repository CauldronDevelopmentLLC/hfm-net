# HFM.NET - Client Monitoring Application for the Folding@Home Distributed Computing Project

[![Build status](https://harlam357.visualstudio.com/hfm-net%20test/_apis/build/status/hfm-net%20(master))](https://harlam357.visualstudio.com/hfm-net%20test/_build/latest?definitionId=0)

Download from Google Drive - https://drive.google.com/open?id=0B8d5F59S5sCiS1RISzdsaEd5UXM&authuser=0

## Version 0.9.8.615

### Release Date: May 12, 2017

* Change: Upgrade to .NET v4.5.2

* Enhancement: Add detection of Windows 10 OS via FAHClient v7 interface.
* Enhancement: Add detection for core 0xA7.

* Fix: On Work Unit History CSV export, format date/time and floating point values using invariant culture.  
       Provides consistent data regardless of the host machine's locale settings.


## Version 0.9.7.558

### Release Date: September 6, 2016

* Change: Reworked HFM's FAH Log File Parsing API.  Changes targeted to make v7 log files a first class citizen in lieu of of being a derivation of v6 logs.
          As a result HFM as a client application does much less repeated parsing and scanning of the log data which results in less memory usage and faster client data processing.
          I'm also curious to see how well GPU work unit failures are now captured from v7 clients.  I don't have any modern GPUs so this is obviously difficult for me to test.

* Change/Fix: Replaced the preferences/configuration storage mechanism (user.config) which has been a source of random problems since HFM's inception.
              Existing preferences will migrate to the new (home rolled) storage mechanism.  The new preferences can be found in the HFM data files folder (config.xml).

* Fix: Crash reported by john on the hfm-net Google Group.  He discovered a race condition between HFM attempting to write to the cached log.txt file while the web
       generation also attempts to upload (read) the same log.txt file via FTP, and vice-versa.  HFM will attempt either operation multiple times before timing out and 
       also fail graciously in lieu of tearing the process down.  Neither I/O operation is critical to the long term health of the process.


## Version 0.9.6.501

### Release Date: June 19, 2016

* Fix: Change the default Project Summary download URL for new installs to http://assign.stanford.edu/api/project/summary.

* Enhancement: Right-click context menu now includes commands to Fold, Pause, and Finish the FAH client slot.
* Enhancement: Work Unit History data can now be exported to csv through the File menu of the Work Unit History window.

* Change: Reworked HFM's FAH v7 client interface API.
* Change: Reworked FAH v7 client handling code.  Testing has shown faster retrieval times of the log.txt file through the FAH v7 client API.


## Version 0.9.5.478

### Release Date: February 7, 2016

* Fix/Update: Use project summary JSON feed for project (work unit) information in lieu of parsing psummary.html.
