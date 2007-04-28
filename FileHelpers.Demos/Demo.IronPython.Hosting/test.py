def main(engine, record) :
    if record.CopyrightDetailCode == "C" :
        print "CINLIST file dated", record.FileVersionDate, "sequence", record.VolumeSequenceNumber
        print "=======================================\n"
        return
    print "CIN:", record.CinCode, "Shape:", record.MailShape, "Discount:", record.DiscountTypeCode, "Sort:", record.SortType.strip()
