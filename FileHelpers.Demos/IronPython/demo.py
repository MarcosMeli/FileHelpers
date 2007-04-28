# load the FileHelpers assembly and import the namespaces
import clr
clr.AddReference("FileHelpers")
from FileHelpers import *
from FileHelpers.RunTime import *

# Read in 2 FileHelpers RunTime class definitions
copyrightRecType = FixedLengthClassBuilder.ClassFromXmlFile(
    "CinListCopyrightRecord.xml")
detailRecType = FixedLengthClassBuilder.ClassFromXmlFile(
    "CinListDetailRecord.xml")

# Define a RecordTypeSelector delegate.
def recordSelector(engine, line) :
    if line[0] == 'C' : return copyrightRecType
    elif line[0] == 'D' : return detailRecType

# Create a MultiRecordEngine, giving it the delegate and classes
engine = MultiRecordEngine(
    RecordTypeSelector(recordSelector), 
    copyrightRecType,
    detailRecType)
# Load the CINLIST file into the engine
engine.BeginReadFile("CINLIST")

# Loop through every record in the CINLIST file:
while engine.ReadNext() != None :
    rec = engine.LastRecord
    # We don't yet know which type of record object this is,
    # but both possible types have a common field called "CopyrightDetailCode"
    # which is a record type indicator.  If this field contains "C"
    # then the record must be a CopyrightRecord - otherwise it's a DetailRecord.
    if rec.CopyrightDetailCode == "C" :
        print "CINLIST file dated", rec.FileVersionDate, "sequence", rec.VolumeSequenceNumber
        print "=======================================\n"
        continue
    print "CIN:", rec.CinCode, "Shape:", rec.MailShape, "Discount:", rec.DiscountTypeCode, "Sort:", rec.SortType.strip()

print "DONE"