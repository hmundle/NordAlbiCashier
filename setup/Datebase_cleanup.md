# Issue description
On 2024-08-05 server side was hanging, users clicked several time the "Bezahlt" button.
At the end all clicks were added to the database as invoices.
This duplicated records need to be deleted again

# SQL statement to show duplicates
- Show deleted as well:
```sql
SELECT 
(SELECT count(*)::int FROM sellings AS s WHERE  i.id = s.invoice_id) AS "Count", 
(SELECT COALESCE(sum(s0.final_price), 0.0) FROM sellings AS s0 WHERE  i.id = s0.invoice_id) AS "Sum",
i.id AS "Id", i.operator AS "Operator", 
i.created AS "Created", i.modified AS "Modified"
FROM invoices AS i
WHERE  i.operator = 'Kasse2' AND i.created >= '2024-08-05T13:28:21.617299Z' AND i.created < '2024-08-05T13:59:44.025571Z'
ORDER BY i.created DESC
```

- Original (without deleted)
```sql
SELECT 
i.id AS "Id", i.type AS "Type", i.comment AS "Comment", i.operator AS "Operator", 
i.created AS "Created", i.modified AS "Modified", 
(
SELECT count(*)::int FROM sellings AS s WHERE NOT (s.is_deleted) AND i.id = s.invoice_id) AS "Count", (
SELECT COALESCE(sum(s0.final_price), 0.0)
FROM sellings AS s0
WHERE NOT (s0.is_deleted) AND i.id = s0.invoice_id) AS "Sum"
FROM invoices AS i
WHERE NOT (i.is_deleted) AND i.operator = 'Kasse2' AND i.created >= '2024-08-05T13:28:21.617299Z' AND i.created < '2024-08-05T13:59:44.025571Z'
ORDER BY i.created DESC
```

# Selected IDs
Kasse 1:
```sql
(
'09939ace-2bbb-417f-936c-f0f8581a0357',
'4e6e36b2-ed8a-474c-b452-837a333cd8b3',
'e48e2f3d-bb33-49c0-a19f-89181804a6f0',
'535f5017-a70b-4dd2-9deb-8289a504fde0',
'612a3db3-2566-4f26-8a5e-083053bec6de',
'b2327aca-c809-455c-bee5-00a80149eeb5',

'f4c39c5a-1e0d-474e-8a02-87b85341fb9c',
'11b4d6a1-3451-414d-a2a0-ee8999ef91d3',
'ea90886f-a208-45e4-9517-487846a154eb',
'8ce1f3f5-e256-4c07-967e-3cf4a0321d27',
'd48b640b-0406-4854-8adc-11e4d77b63cd',
'581616ce-75e7-41bc-a424-24a72a423db0',
'ee3d4391-0cbe-4f1d-8734-797fb22ad24f',
'e82295b7-7930-4e60-8290-197eea3d9407',
'435f5e51-644a-4cfd-ba2b-0787981a9ce2',
'd71658ee-38fb-48f1-811b-4c11bb42445d',
'5069f0da-c670-4f18-9a99-4ada5a6001d6',
'9156cf52-10ff-43fc-a634-101b9b88c8d3',
'a404fd2f-0f92-4b3e-a3fc-dc54c816ee75',
'f8b39f02-e0ef-4805-9537-d65c1d085f82',
'6f677ce3-6889-423a-8736-65f0a101a117',
'ce07fd65-6e14-46a6-9303-cf12fd130095',
'0bad09fb-7df2-44af-820d-279e78a96a25',
'7e587717-da8c-4f00-ae7b-7518a8dc85d2',
'95166e6c-1514-4ae1-81b2-396c8c6e0c37',
'9283b524-72aa-43db-aa81-2ef7042e029d',
'c74a538b-a1ef-4cfe-b56b-67155470e3bd',
'c0b176a4-80f8-4a41-b105-b5d1343a6b86',
'31266f27-4104-4388-8a4c-69fbed186061',
'bac9ad17-c8e8-4abe-9e8d-f77bfe3caa83',
'484aaa2d-69e2-46e2-81e7-93694f59087d',
'd96196db-f680-465c-8b3c-dcecce2a4a18',
'cbdfe3a7-5912-4f12-a99f-a167e077e977',
'43b08bd1-8629-47e7-acd0-e2d094bba0c6',
'7ec9cfea-a9cc-4afa-ac70-841bdf722061',
'4848ab6a-8c79-426a-8046-aee95bcd1907',
'620011c3-2ac3-40e7-817b-e0a8839578cc',
'955aa08f-16a6-48de-aeed-6230437da7c9',
'9559912b-b495-4c6c-b47d-c7697028a54d',
'4b71bdb7-23a4-4e84-9176-ae1b7fcd9fd6',
'19106e72-0afa-4a04-a02a-4b82acb5d3c0',
'665b522e-d45e-4d6e-96cc-4a774e1c9b6c',
'1ebde859-4870-4fc7-9d95-9926ca332d44',
'1ab148a2-2af9-436e-a073-ef878a88ea86',
'f21c7930-ecac-4bed-9826-f06ea10d7871',
'31140ca6-67f6-4d22-b67b-109a30a9e942',
'7e9f9948-f180-475a-bef3-fa53781c6151',
'6b94cc9a-0f4b-4764-8f2a-d3500260af0b',
'064a6c0a-67d7-41ff-9f13-f4a47ba2008a',
'a2f16470-198c-4cdb-b9ba-0937b6a54daa',
'226aaa60-6882-42fc-973d-d851b0223fd2',
'6607f886-9a22-4898-b4e7-e278a8142ea4',
'7cf17046-f311-436d-a366-cb3052ffebe8',
'8f990f4a-04ac-4306-9fb8-3ede69bdab6b',
'0423d9c6-ee40-4f3e-b00e-64c0e7a58998',
'5cffce72-b4b4-4b55-b56a-f3efe912e0e0',
'5fcbcbf9-7d52-40fa-af83-eb751a1600ee',

'331d235e-60aa-41c7-9ce2-fcb6c521d1a4',
'057a781d-81cc-4f34-9cec-bde33204d0d2',
'2378a0c9-adf1-4eb8-ade8-ea2834f70b3e',
'ac734638-0079-4225-8573-243495761f60',
'58577dd8-64af-43ff-b399-6a273b26c4c3'
)
```

Kasse 2:
```sql
(
'c9f89610-875b-4f0e-9eeb-fa41620d968e',

'f7538fa4-5702-47d2-bc99-f5825218f73a',

'b11aa0a0-14ad-4ab2-a574-0aa08c5dfb65',
'4c2a840f-5463-484b-8022-f391b04952a2',
'514593be-3a97-4caa-87ba-93717ae850fe',
'aba8db3d-d6dd-44ac-8d0a-9c591b4d58d0',
'ddb148e2-2615-476b-9fac-d02b5cbec592',
'beffab94-5205-427a-9b01-b565291837be'
)
```

Combine with one of:
```sql
SELECT *
FROM public.sellings 
WHERE invoice_id IN
```

```sql
SELECT *
FROM public.invoices 
WHERE id IN
```

# Delete duplicated records
Combine with
```sql
DELETE
FROM public.sellings 
WHERE invoice_id IN
```

```sql
DELETE
FROM public.invoices 
WHERE id IN
```


