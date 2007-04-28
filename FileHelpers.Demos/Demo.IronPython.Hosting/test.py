def main(engine, record) :
    # We don't yet know which type of record object this is,
    # but both possible types have a common field called "CopyrightDetailCode"
    # which is a record type indicator.  If this field contains "C"
    # then the record must be a CopyrightRecord - otherwise it's a DetailRecord.
    if record.CopyrightDetailCode == "C" :
        print "CINLIST file dated", record.FileVersionDate, "sequence", record.VolumeSequenceNumber
        print "=======================================\n"
        return
    print "CIN:", record.CinCode, "Shape:", record.MailShape, "Discount:", record.DiscountTypeCode, "Sort:", record.SortType.strip()
