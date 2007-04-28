import clr
clr.AddReference("FileHelpers")
from FileHelpers import *
from FileHelpers.RunTime import *

copyrightRecType = FixedLengthClassBuilder.ClassFromXmlFile(
    "CinListCopyrightRecord.xml")
detailRecType = FixedLengthClassBuilder.ClassFromXmlFile(
    "CinListDetailRecord.xml")

def recordSelector(engine, line) :
    if line[0] == 'C' : return copyrightRecType
    elif line[0] == 'D' : return detailRecType

engine = MultiRecordEngine(
    RecordTypeSelector(recordSelector), 
    copyrightRecType,
    detailRecType)

engine.BeginReadFile("CINLIST")

while engine.ReadNext() != None :
    rec = engine.LastRecord
    if rec.CopyrightDetailCode == "C" :
        print "CINLIST file dated", rec.FileVersionDate, "sequence", rec.VolumeSequenceNumber
        print "=======================================\n"
        continue
    print "CIN:", rec.CinCode, "Shape:", rec.MailShape, "Sort:", rec.SortType

print "DONE"