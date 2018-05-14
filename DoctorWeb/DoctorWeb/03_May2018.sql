Create view LastPrescriptions
as

WITH ranked_messages AS (
  SELECT m.*, ROW_NUMBER() OVER (PARTITION BY m.PatientId ORDER BY m.[Date] DESC) AS rn
  FROM Prescriptions AS m
)
SELECT * FROM ranked_messages WHERE rn = 1;

go
 
create Procedure getPatientSearchResult
@PatientName varchar(200)
as
begin
 
Select p.Status,p.Name,P.Contact as MobileNo,p.Address,p.Id,d.Name as DepartmentName, r.Name as Reference,
isnull(dr.Name,'Other') as RefferalName , l.ID as InvoiceNo, l.Date,l.Diagnosis,l.[Procedure] , isnull(pt.PatientTypeName,'') as PatientType,
case when l.Id is null then 'New Patient' else  'FollowUP' end as NewPatientOrFollowUp ,'' as ChargesType
from Patients p
inner join Departments d on p.DepartmentID=d.ID
inner join ReferredBies r on r.ID=p.ReferredByID
left outer join Doctors dr on dr.ID=p.DoctorID
left outer join LastPrescriptions l on p.Id = l.PatientId
left outer join PatientTypes pt on l.PatientTypeID = pt.ID
 where
p.Name + p.Address + cast(p.[status] as varchar) like '%' + @PatientName +'%'

 end
  go
   