InrixDataRetrieval
==================

The module has the code to retrieve travel time data for each roadway from InRix company. 
The program querys data from InRix API periodically, parses the retrieved XML, and stores into the database.
In the main GUI, the latest retrieved records are shown on the data table.

Before the process is started, it should be configured to set the InRix account information, 
the local database settings, the updating cycle, log file path, and other parameters. 
All these parameters can be set by clicking File -> Settings.


Author: Kevin Lu
Date: 2011
