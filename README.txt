WinLink Importer to WebEOC
Written By Sam Miller sam.miller@tdem.texas.gov

Decription:
Written to take an xml form and import into a WebEOC board. 


INSTALL
- Place directory containing the compliled application in a reachable folder for iis
- Edit Web.config
-- Make sure to ajust all the WebEOC User specific board references
-- Ajust the URL of the WebEOC API (this technicly could be an ip if internet was down and it needed to be uploaded from an intranet)
- Edit FieldMatchUp.csv, the first value is the xml field value, the second is the corrisponding WebEOC field


