using Nac.Models.Entities.Owned;

namespace Nac.Dal.Initialization;

public static class SampleData
{
    public static List<User> Users => new()
    {
        new() {Id = 1, Name = "Frithjof" },
        new() {Id = 2, Name = "Leslie" },
        new() {Id = 3, Name = "Nina" },
        new() {Id = 4, Name = "Heiko" },
    };

    public static List<Tape> Tapes => new()
    {
        new() {Id = 1, TapeLabel = "EMNZ97_122" },
        new() {Id = 2, TapeLabel = "EM98NZ_IRS1D_D023" },
        new() {Id = 3, TapeLabel = "EM98NZ_IRS1D_D027" },
        new() {Id = 4, TapeLabel = "EMNZ97_123" },
        new() {Id = 5, TapeLabel = "EM98NZ_IRS1D_D033" },
        new() {Id = 6, TapeLabel = "EMNZ97_223", IsDeleted = true },
    };
    public static List<Pass> Passes => new()
    {
        new() {Id = 1, TapeId = 2, AcquisitionDate = DateOnly.Parse("1998-01-11"), AOS = DateTimeOffset.Parse("1998-01-11T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-01-11T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1C, OrbitNumber = 243, StartCount = 514, StopCount = 767, Stream = StreamType.LISS_III_WIFS },
        new() {Id = 2, TapeId = 2, AcquisitionDate = DateOnly.Parse("1998-02-11"), AOS = DateTimeOffset.Parse("1998-02-11T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-02-11T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1D, OrbitNumber = 343, StartCount = 414, StopCount = 667, Stream = StreamType.PAN },
        new() {Id = 3, TapeId = 3, AcquisitionDate = DateOnly.Parse("1998-01-16"), AOS = DateTimeOffset.Parse("1998-01-16T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-01-16T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1D, OrbitNumber = 443, StartCount = 614, StopCount = 797, Stream = StreamType.PAN },
        new() {Id = 4, TapeId = 1, AcquisitionDate = DateOnly.Parse("1997-01-11"), AOS = DateTimeOffset.Parse("1997-01-11T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1997-01-11T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1D, OrbitNumber = 753, StartCount = 814, StopCount = 967, Stream = StreamType.LISS_III_WIFS },
        new() {Id = 5, TapeId = 5, AcquisitionDate = DateOnly.Parse("1998-01-12"), AOS = DateTimeOffset.Parse("1998-01-12T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-01-12T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1C, OrbitNumber = 244, StartCount = 504, StopCount = 717, Stream = StreamType.LISS_III_WIFS },
        new() {Id = 6, TapeId = 3, AcquisitionDate = DateOnly.Parse("1998-02-15"), AOS = DateTimeOffset.Parse("1998-02-15T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-02-15T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1D, OrbitNumber = 345, StartCount = 604, StopCount = 817, Stream = StreamType.LISS_III_WIFS },
        new() {Id = 7, TapeId = 1, AcquisitionDate = DateOnly.Parse("1998-03-11"), AOS = DateTimeOffset.Parse("1998-03-11T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-03-11T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1C, OrbitNumber = 444, StartCount = 704, StopCount = 917, Stream = StreamType.PAN },
        new() {Id = 8, TapeId = 4, AcquisitionDate = DateOnly.Parse("1998-04-11"), AOS = DateTimeOffset.Parse("1998-04-11T10:15:01Z").UtcDateTime, LOS = DateTimeOffset.Parse("1998-04-11T10:25:23Z").UtcDateTime, Mission = MissionIdentifier.IRS_1D, OrbitNumber = 543, StartCount = 804, StopCount = 917, Stream = StreamType.PAN },
    };

    private static Proc GetProc(ProcessingResult resultStatus = ProcessingResult.Success)
    {
        return new() { TimeStart = DateTimeOffset.Parse("2022-06-11T10:15:01+00:00").UtcDateTime, TimeEnd = DateTimeOffset.Parse("2022-06-11T10:15:19+00:00").UtcDateTime, ResultStatus = resultStatus, DestPath = "/base/sub/path/", SwVersion = "PPAS_v4.3.32_b5342" };
    }
    private static Proc GetProcNull()
    {
        return new() { SwVersion = "PPAS_v4.3.32_b5342" };
    }

    private static TdasProcessingEnvironment tpe => new() { TdasSwVersion = "1.5.32_b534", TdasSwModuleA = "1.5.37_b634", TdasSwModuleB = "1.5.22_b234" };
    private static TProcessingEnvironmentGaf tpeg => new() { RecorderId = "R2", BufferId = "B3", TctId = "T1", EclId = "C2" };
    private static TInputParam tip => new() { InParamTA = "xxx", InParamTB = "yyy" };
    private static TOutputQuality toq => new() { OutParamTA = "zzz", OutParamTB = "xyxy" };
    public static List<TProduct> TProducts => new()
    {
        new() {ProcId = new("57B3B5F6-77B5-43C8-8FD4-DF312C201257"), PassId = 2, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc() },
        new() {ProcId = new("2E915FEA-0E10-4AF3-9D0F-3E4E5AF7B857"), PassId = 1, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc() },
        new() {ProcId = new("BF46ED54-6909-43EC-AF7C-3581F08D5824"), PassId = 3, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc() },
        new() {ProcId = new("F00DCA8C-1D22-417E-8275-D25897FD4F44"), PassId = 5, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143"), PassId = 4, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProcNull() },
        new() {ProcId = new("57B3B5F6-7FFF-43C8-8FD4-DF312C201257"), PassId = 2, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 1 },
        new() {ProcId = new("8F5DC134-8460-4B68-9E71-481935720333"), PassId = 8, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc(ProcessingResult.Terminated) },
        new() {ProcId = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94"), PassId = 7, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc() },
        new() {ProcId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B"), PassId = 6, ProcessingEnvironment = tpe, ProcessingEnvironmentGaf = tpeg, InputParam = tip, OutputQuality = toq, Proc = GetProc() },
    };
    private static L0InputParam l0ip => new() { InParamL0A = "xxxYY", InParamL0B = "yyyYY" };
    private static L0OutputQuality l0oq => new() { OutParamL0A = "zzzYY", OutParamL0B = "xyxyYY" };
    private static L0Output l0op
    {
        get
        {
            L0Output ob = new() { CenterTime = DateTimeOffset.Parse("1998-01-11T23:15:01Z").UtcDateTime, SunAzimuth = 145.67, SunElevation = 67.89, Path = 29, FootprintCenter = "<gml:pos>61.68591 49.98658</gml:pos>", FootprintOutline = "<gml:posList>69.48746 36.56942 56.97692 33.64624 53.41834 57.64156 64.32809 70.72876 69.48746 36.56942</gml:posList>" };
            ob.StartTime = ob.CenterTime - new TimeSpan(1, 30, 0);
            ob.EndTime = ob.CenterTime + new TimeSpan(1, 30, 0);
            return ob;
        }
    }
    public static List<L0Product> L0Products => new()
    {
        new() {ProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B5F6-77B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc() },
        new() {ProcId = new("2E915FEA-1E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915FEA-0E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED54-6909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA8C-2D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA8C-1D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8652-DD87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B5F6-8FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("8F5DC134-8460-4B68-9E71-481935720333") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 2 },

        new() {ProcId = new("57B3B522-87B5-43C8-8FD4-DF312C201257"), PrecProcId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc() },
        new() {ProcId = new("2E915F22-1E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("8F5DC134-8460-4B68-9E71-481935720333") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("BF46ED22-7909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED54-6909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA22-2D22-417E-8275-D25897FD4F44"), PrecProcId = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8622-DD87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B522-8FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 3 },

        new() {ProcId = new("57B3B533-87B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B5F6-77B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc() },
        new() {ProcId = new("2E915F33-1E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915FEA-0E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("BF46ED33-7909-43EC-AF7C-3581F08D5824"), PrecProcId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA33-2D22-417E-8275-D25897FD4F44"), PrecProcId = new("8F5DC134-8460-4B68-9E71-481935720333") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8633-DD87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B533-8FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc(ProcessingResult.Terminated) },

        new() {ProcId = new("57B3B544-87B5-43C8-8FD4-DF312C201257"), PrecProcId = new("7A9265E9-9B83-43F6-BB9E-F4BABD468B94") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc() },
        new() {ProcId = new("2E915F44-1E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("648D580D-B57D-43AF-8981-5E9F96E3CC2B") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("BF46ED44-7909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED54-6909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA44-2D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA8C-1D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8644-DD87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-CD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B544-8FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("8F5DC134-8460-4B68-9E71-481935720333") , ProcessingEnvironment = tpe, InputParam = l0ip, OutputQuality = l0oq, Output = l0op, Proc = GetProc(ProcessingResult.Terminated) },
    };

    public static List<L0Archiving> L0Archivings => new()
    {
        new() {Id = 1, L0ProductProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 4 },
        new() {Id = 2, L0ProductProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 3, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(ProcessingResult.Terminated) },
        new() {Id = 4, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc() },
    };
    public static List<L0Delivery> L0Deliveries => new()
    {
        new() {Id = 1, L0ProductProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 2, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), DeliveryType = DeliveryType.NRSC },
        new() {Id = 3, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), DeliveryType = DeliveryType.ESA },
    };
    private static QCStep qcstepFailed => new() { Status = QualityControlStatus.Failed, Description = "this is Failed", Operator = "Mr. Failed", Time = DateTimeOffset.Parse("2022-07-01T10:15:19+00:00").UtcDateTime };
    private static QCStep qcstepFailedClarified => new() { Status = QualityControlStatus.FailedClarified, Description = "this is FailedClarified", Operator = "Mr. FailedClarified", Time = DateTimeOffset.Parse("2022-07-02T10:15:19+00:00").UtcDateTime };
    private static QCStep qcstepPassed => new() { Status = QualityControlStatus.Passed, Description = "this is Passed", Operator = "Mr. Passed", Time = DateTimeOffset.Parse("2022-07-03T10:15:19+00:00").UtcDateTime };
    private static QCStep qcstepOpen => new() { Status = QualityControlStatus.Open, Description = "this is Open", Operator = "Mr. Open", Time = DateTimeOffset.Parse("2022-07-04T10:15:19+00:00").UtcDateTime };
    private static QCStep qcstepInProgress => new() { Status = QualityControlStatus.InProgress, Description = "this is InProgress", Operator = "Mr. InProgress", Time = DateTimeOffset.Parse("2022-07-05T10:15:19+00:00").UtcDateTime };
    private static QCStep qcstepNoCheck => new() { Status = QualityControlStatus.NoCheck, Description = "this is NoCheck", Operator = "Mr. NoCheck", Time = DateTimeOffset.Parse("2022-07-06T10:15:19+00:00").UtcDateTime };

    public static List<L0QualityControl> L0QualityControls => new()
    {
        new() {Id = 1, L0ProductProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC10_L0AutoNotZero = qcstepFailedClarified },
        new() {Id = 2, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC09_L0AutoAllFiles = qcstepFailed },

        new() {Id = 3, L0ProductProcId = new("57B3B522-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC02_L0SipMeta = qcstepFailedClarified },
        new() {Id = 4, L0ProductProcId = new("2E915F22-1E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC01_L0Browse = qcstepFailed },
        new() {Id = 5, L0ProductProcId = new("BF46ED22-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC10_L0AutoNotZero = qcstepFailedClarified },
        new() {Id = 6, L0ProductProcId = new("F00DCA22-2D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC09_L0AutoAllFiles = qcstepInProgress, ProcErrorId = 9 },
        new() {Id = 7, L0ProductProcId = new("5C7C8622-DD87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC02_L0SipMeta = qcstepNoCheck },
        new() {Id = 8, L0ProductProcId = new("57B3B522-8FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC01_L0Browse = qcstepOpen },

        new() {Id = 13, L0ProductProcId = new("57B3B522-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC10_L0AutoNotZero = qcstepPassed },
        new() {Id = 14, L0ProductProcId = new("2E915F22-1E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC09_L0AutoAllFiles = qcstepFailed },
        new() {Id = 15, L0ProductProcId = new("BF46ED22-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC02_L0SipMeta = qcstepFailedClarified },
        new() {Id = 16, L0ProductProcId = new("F00DCA22-2D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC01_L0Browse = qcstepInProgress, ProcErrorId = 10 },
        new() {Id = 17, L0ProductProcId = new("5C7C8622-DD87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC10_L0AutoNotZero = qcstepNoCheck },
        new() {Id = 18, L0ProductProcId = new("57B3B522-8FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC09_L0AutoAllFiles = qcstepOpen },

        new() {Id = 33, L0ProductProcId = new("57B3B533-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC02_L0SipMeta = qcstepPassed },
        new() {Id = 34, L0ProductProcId = new("2E915F33-1E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC01_L0Browse = qcstepFailed },
        new() {Id = 35, L0ProductProcId = new("BF46ED33-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC10_L0AutoNotZero = qcstepFailedClarified },
        new() {Id = 36, L0ProductProcId = new("F00DCA33-2D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC09_L0AutoAllFiles = qcstepInProgress, ProcErrorId = 11 },
        new() {Id = 37, L0ProductProcId = new("5C7C8633-DD87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC02_L0SipMeta = qcstepNoCheck },
        new() {Id = 38, L0ProductProcId = new("57B3B533-8FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC01_L0Browse = qcstepOpen },

        new() {Id = 43, L0ProductProcId = new("57B3B544-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC10_L0AutoNotZero = qcstepPassed, QC01_L0Browse = qcstepNoCheck },
        new() {Id = 44, L0ProductProcId = new("2E915F44-1E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC09_L0AutoAllFiles = qcstepFailed, QC02_L0SipMeta = qcstepOpen },
        new() {Id = 45, L0ProductProcId = new("BF46ED44-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC02_L0SipMeta = qcstepFailedClarified, QC09_L0AutoAllFiles = qcstepPassed },
        new() {Id = 46, L0ProductProcId = new("F00DCA44-2D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC01_L0Browse = qcstepInProgress, QC10_L0AutoNotZero = qcstepNoCheck, ProcErrorId = 12 },
        new() {Id = 47, L0ProductProcId = new("5C7C8644-DD87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC10_L0AutoNotZero = qcstepNoCheck, QC01_L0Browse = qcstepNoCheck },
        new() {Id = 48, L0ProductProcId = new("57B3B544-8FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC09_L0AutoAllFiles = qcstepOpen, QC02_L0SipMeta = qcstepNoCheck, QC01_L0Browse = qcstepPassed, QC10_L0AutoNotZero = qcstepNoCheck }
    };
    public static List<L0Sip> L0Sips => new()
    {
        new() {Id = 1, L0ProductProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 2, L0ProductProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824"), Proc = GetProc() },
    };
    private static LdpgsProcessingEnvironment lpe => new() { LdpgsSwVersion = "1.3.12_b534", LdpgsSwModuleA = "1.3.12_b634", LdpgsSwModuleB = "1.3.12_b234" };
    private static L1InputParam l1ip => new() { InParamL1A = "xxxXX", InParamL1B = "yyyXX" };
    private static L1OutputQuality l1oq => new() { OutParamL1A = "zzzXX", OutParamL1B = "xyxyXX" };
    private static L1Output l1op
    {
        get
        {
            L1Output ob = new() { SensorHead = SensorHeadType.A, Row = 2345, Quadrant = QuadrantType._00, SubScene = SubSceneType.M, CenterTime = DateTimeOffset.Parse("1998-01-11T22:15:01Z").UtcDateTime, SunAzimuth = 156.78, SunElevation = 56.78, Path = 43, FootprintCenter = "<gml:pos>61.68591 49.98658</gml:pos>", FootprintOutline = "<gml:posList>69.48746 36.56942 56.97692 33.64624 53.41834 57.64156 64.32809 70.72876 69.48746 36.56942</gml:posList>" };
            ob.StartTime = ob.CenterTime - new TimeSpan(2, 30, 0);
            ob.EndTime = ob.CenterTime + new TimeSpan(2, 30, 0);
            return ob;
        }
    }
    public static List<L1Product> L1Products => new()
    {
        new() {ProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("2E915FEA-2E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915FEA-1E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("BF46ED54-8909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA8C-3D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA8C-2D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8652-ED87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-DD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B5F6-9FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B522-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc(ProcessingResult.Terminated) },

        new() {ProcId = new("57B3B522-97B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B522-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("2E915F22-2E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915F22-1E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("BF46ED22-8909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED22-7909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA22-3D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA22-2D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8622-ED87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8622-DD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B522-9FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B522-8FFF-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 5 },

        new() {ProcId = new("57B3B533-97B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B533-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("2E915F33-2E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915F33-1E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("BF46ED33-8909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED33-7909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA33-3D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA33-2D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8633-ED87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8633-DD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B533-9FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B533-8FFF-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 6 },

        new() {ProcId = new("57B3B544-97B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B544-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("2E915F44-2E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915F44-1E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("BF46ED44-8909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED44-7909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA44-3D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA44-2D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8644-ED87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8644-DD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B544-9FFF-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B544-8FFF-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l1ip, OutputQuality = l1oq, Output = l1op, Proc = GetProc(ProcessingResult.Terminated) },

    };
    public static List<L1Archiving> L1Archivings => new()
    {
        new() {Id = 1, L1ProductProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 7 },
        new() {Id = 2, L1ProductProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 3, L1ProductProcId = new("F00DCA8C-3D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Terminated) },
        new() {Id = 4, L1ProductProcId = new("F00DCA8C-3D22-417E-8275-D25897FD4F44"), Proc = GetProc() },
    };
    public static List<L1Delivery> L1Deliveries => new()
    {
        new() {Id = 1, L1ProductProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 2, L1ProductProcId = new("5C7C8652-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(), DeliveryType = DeliveryType.NRSC },
        new() {Id = 3, L1ProductProcId = new("5C7C8652-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(), DeliveryType = DeliveryType.ESA },
    };
    public static List<L1QualityControl> L1QualityControls => new()
    {
        new() {Id = 1, L1ProductProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 2, L1ProductProcId = new("F00DCA8C-3D22-417E-8275-D25897FD4F44"), Proc = GetProc() },

        new() {Id = 3, L1ProductProcId = new("57B3B522-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC03_L1BrowseLoc = qcstepFailedClarified },
        new() {Id = 4, L1ProductProcId = new("2E915F22-2E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC04_L1BrowseImgErr = qcstepFailed },
        new() {Id = 5, L1ProductProcId = new("BF46ED22-8909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC05_L1ProdFullRes = qcstepFailedClarified },
        new() {Id = 6, L1ProductProcId = new("F00DCA22-3D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC06_L1ProdMeta = qcstepInProgress, ProcErrorId = 13 },
        new() {Id = 7, L1ProductProcId = new("5C7C8622-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC07_L1SipImage = qcstepNoCheck },
        new() {Id = 8, L1ProductProcId = new("57B3B522-9FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC08_L1SipMeta = qcstepOpen },

        new() {Id = 13, L1ProductProcId = new("57B3B522-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC11_L1AutoAllFiles = qcstepPassed },
        new() {Id = 14, L1ProductProcId = new("2E915F22-2E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC12_L1AutoNotZero = qcstepFailed },
        new() {Id = 15, L1ProductProcId = new("BF46ED22-8909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC03_L1BrowseLoc = qcstepFailedClarified },
        new() {Id = 16, L1ProductProcId = new("F00DCA22-3D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC04_L1BrowseImgErr = qcstepInProgress, ProcErrorId = 14 },
        new() {Id = 17, L1ProductProcId = new("5C7C8622-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC05_L1ProdFullRes = qcstepNoCheck },
        new() {Id = 18, L1ProductProcId = new("57B3B522-9FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC06_L1ProdMeta = qcstepOpen },

        new() {Id = 33, L1ProductProcId = new("57B3B533-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC07_L1SipImage = qcstepPassed },
        new() {Id = 34, L1ProductProcId = new("2E915F33-2E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC08_L1SipMeta = qcstepFailed },
        new() {Id = 35, L1ProductProcId = new("BF46ED33-8909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC11_L1AutoAllFiles = qcstepFailedClarified },
        new() {Id = 36, L1ProductProcId = new("F00DCA33-3D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC12_L1AutoNotZero = qcstepInProgress, ProcErrorId = 15 },
        new() {Id = 37, L1ProductProcId = new("5C7C8633-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC03_L1BrowseLoc = qcstepNoCheck },
        new() {Id = 38, L1ProductProcId = new("57B3B533-9FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC04_L1BrowseImgErr = qcstepOpen },

        new() {Id = 43, L1ProductProcId = new("57B3B544-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC05_L1ProdFullRes = qcstepPassed, QC08_L1SipMeta = qcstepNoCheck },
        new() {Id = 44, L1ProductProcId = new("2E915F44-2E10-4AF3-9D0F-3E4E5AF7B857"), Proc = GetProc(ProcessingResult.Terminated), QC06_L1ProdMeta = qcstepFailed, QC07_L1SipImage = qcstepOpen },
        new() {Id = 45, L1ProductProcId = new("BF46ED44-8909-43EC-AF7C-3581F08D5824"), Proc = GetProc(), QC07_L1SipImage = qcstepFailedClarified, QC05_L1ProdFullRes = qcstepPassed },
        new() {Id = 46, L1ProductProcId = new("F00DCA44-3D22-417E-8275-D25897FD4F44"), Proc = GetProc(ProcessingResult.Failed), QC08_L1SipMeta = qcstepInProgress, QC04_L1BrowseImgErr = qcstepNoCheck, ProcErrorId = 16 },
        new() {Id = 47, L1ProductProcId = new("5C7C8644-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc(ProcessingResult.Terminated), QC11_L1AutoAllFiles = qcstepNoCheck, QC03_L1BrowseLoc = qcstepNoCheck },
        new() {Id = 48, L1ProductProcId = new("57B3B544-9FFF-43C8-8FD4-DF312C201257"), Proc = GetProc(), QC12_L1AutoNotZero = qcstepOpen, QC06_L1ProdMeta = qcstepNoCheck, QC04_L1BrowseImgErr = qcstepPassed, QC03_L1BrowseLoc = qcstepNoCheck }
    };
    public static List<L1Sip> L1Sips => new()
    {
        new() {Id = 1, L1ProductProcId = new("57B3B5F6-97B5-43C8-8FD4-DF312C201257"), Proc = GetProc() },
        new() {Id = 2, L1ProductProcId = new("5C7C8652-ED87-4AAB-B820-87BF7DAC9143"), Proc = GetProc() },
    };
    private static L2InputParam l2ip => new() { InParamL2A = "xxxZZ", InParamL2B = "yyyZZ" };
    private static L2OutputQuality l2oq => new() { OutParamL2A = "zzzZZ", OutParamL2B = "xyxyZZ" };
    public static List<L2Product> L2Products => new()
    {
        new() {ProcId = new("57B3B5F6-A7B5-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("2E915FEA-3E10-4AF3-9D0F-3E4E5AF7B857"), PrecProcId = new("2E915FEA-1E10-4AF3-9D0F-3E4E5AF7B857") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProc() },
        new() {ProcId = new("BF46ED54-9909-43EC-AF7C-3581F08D5824"), PrecProcId = new("BF46ED54-7909-43EC-AF7C-3581F08D5824") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("F00DCA8C-4D22-417E-8275-D25897FD4F44"), PrecProcId = new("F00DCA8C-2D22-417E-8275-D25897FD4F44") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("5C7C8652-FD87-4AAB-B820-87BF7DAC9143"), PrecProcId = new("5C7C8652-DD87-4AAB-B820-87BF7DAC9143") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProcNull() },
        new() {ProcId = new("57B3B5F6-AFFF-43C8-8FD4-DF312C201257"), PrecProcId = new("57B3B5F6-87B5-43C8-8FD4-DF312C201257") , ProcessingEnvironment = lpe, InputParam = l2ip, OutputQuality = l2oq, Output = l1op, Proc = GetProc(ProcessingResult.Failed), ProcErrorId = 8 },
    };

    public static List<ErrorCode> ErrorCodes => new()
    {
        new() {Id = 1, Code = 1001, Description = "Undefined error"},
        new() {Id = 2, Code = 1002, Description = "User error, as common as usual..."},
        new() {Id = 3, Code = 5003, Description = "This is going to happen, when...3"},
        new() {Id = 4, Code = 5004, Description = "This is going to happen, when...4"},
        new() {Id = 5, Code = 5005, Description = "This is going to happen, when...5"},
        new() {Id = 6, Code = 5006, Description = "This is going to happen, when...6"},
        new() {Id = 7, Code = 5007, Description = "This is going to happen, when...7"},
        new() {Id = 8, Code = 5008, Description = "This is going to happen, when...8"},
    };

    public static List<ProcError> ProcErrors => new()
    {
        new() {Id = 1, ErrorCodeId = 3, ErrorMsg = "It was going completely wrong 1!", Description = "Couldn't be done better...1", ProcType = ProcessingType.Transcription, Status = ProcErrorStatus.Fixed, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 2, ErrorCodeId = 2, ErrorMsg = "It was going completely wrong 2!", Description = "Couldn't be done better...2", ProcType = ProcessingType.L0, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 3, ErrorCodeId = 1, ErrorMsg = "It was going completely wrong 3!", Description = "Couldn't be done better...3", ProcType = ProcessingType.L0, Status = ProcErrorStatus.Fixed, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 4, ErrorCodeId = 4, ErrorMsg = "It was going completely wrong 4!", Description = "Couldn't be done better...4", ProcType = ProcessingType.L0_Arch, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 5, ErrorCodeId = 5, ErrorMsg = "It was going completely wrong 5!", Description = "Couldn't be done better...5", ProcType = ProcessingType.L1, Status = ProcErrorStatus.Fixed, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 6, ErrorCodeId = 6, ErrorMsg = "It was going completely wrong 6!", Description = "Couldn't be done better...6", ProcType = ProcessingType.L1, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 7, ErrorCodeId = 7, ErrorMsg = "It was going completely wrong 7!", Description = "Couldn't be done better...7", ProcType = ProcessingType.L1_Arch, Status = ProcErrorStatus.Fixed, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 8, ErrorCodeId = 8, ErrorMsg = "It was going completely wrong 8!", Description = "Couldn't be done better...8", ProcType = ProcessingType.L2, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 9, ErrorCodeId = 1, ErrorMsg = "It was going completely wrong 9!", Description = "Couldn't be done better...9", ProcType = ProcessingType.L0_QC_SIP, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 10, ErrorCodeId = 1, ErrorMsg = "It was going completely wrong 10!", Description = "Couldn't be done better...10", ProcType = ProcessingType.L0_QC, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 11, ErrorCodeId = 8, ErrorMsg = "It was going completely wrong 11!", Description = "Couldn't be done better...11", ProcType = ProcessingType.L0_QC_SIP, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 12, ErrorCodeId = 3, ErrorMsg = "It was going completely wrong 12!", Description = "Couldn't be done better...12", ProcType = ProcessingType.L0_QC, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 13, ErrorCodeId = 6, ErrorMsg = "It was going completely wrong 13!", Description = "Couldn't be done better...13", ProcType = ProcessingType.L1_QC_SIP, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 14, ErrorCodeId = 2, ErrorMsg = "It was going completely wrong 14!", Description = "Couldn't be done better...14", ProcType = ProcessingType.L1_QC, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 15, ErrorCodeId = 4, ErrorMsg = "It was going completely wrong 15!", Description = "Couldn't be done better...15", ProcType = ProcessingType.L1_QC_SIP, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
        new() {Id = 16, ErrorCodeId = 3, ErrorMsg = "It was going completely wrong 16!", Description = "Couldn't be done better...16", ProcType = ProcessingType.L1_QC, Status = ProcErrorStatus.Opened, OpeningTime = DateTimeOffset.Parse("2022-06-11T14:15:19+02:00").UtcDateTime, ReportingTime = DateTimeOffset.Parse("2022-06-11T15:15:19+02:00").UtcDateTime, FixingTime = DateTimeOffset.Parse("2022-06-11T17:15:19+02:00").UtcDateTime },
    };
}
