
-- Them vào quotation : nguon_bg, nguoi_can_thiep
--1 Tổng hợp
SELECT       EM.IDEMP, EM.StaffName, COUNT(DISTINCT EC.idcustomer) AS total_customer_count, 
SUM(CASE WHEN (C.date1 > dateadd(day, - 30, getDate()) AND EC.status = 'true') THEN 1 ELSE 0 END) AS new_customer, 
                         COUNT(DISTINCT CC.idcustomer) AS customer_takecare, COUNT(DISTINCT EC.idcustomer) - COUNT(DISTINCT ISNULL(CC.idcustomer, 0)) AS customer_not_takecare, 
                         COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000006', 'ST000007', 'ST000008')) THEN Q.idexport ELSE NULL END) AS required, COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000006')) 
                         THEN Q.idexport ELSE NULL END) AS required_ch, COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000007')) THEN Q.idexport ELSE NULL END) AS required_tn, 
                         COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000008')) THEN Q.idexport ELSE NULL END) AS required_ktn, COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000001')) THEN Q.idexport ELSE NULL END) 
                         AS hg, COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000002')) THEN Q.idexport ELSE NULL END) AS bg, COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000004')) THEN Q.idexport ELSE NULL END) AS po, 
                         COUNT(DISTINCT CASE WHEN (Q.idstatusquotation IN ('ST000005')) THEN Q.idexport ELSE NULL END) AS tb, SUM(CASE WHEN (Q.idstatusquotation IN ('ST000002')) THEN Q.total ELSE 0 END) AS total_bq, 
                         SUM(CASE WHEN (Q.idstatusquotation IN ('ST000004')) THEN Q.total ELSE 0 END) AS total_po
FROM            dbo.EMPLOYEES AS EM WITH (nolock) LEFT OUTER JOIN
                             (SELECT       Q.dateimport, Q.idcustomer, Q.IDEMP, Q.idexport, Q.invoice, Q.idstatusquotation, Q.idquotationtype, Q.quotationno, Q.invoiceeps, Q.datepo, Q.idemppo, Q.idpotype, Q.last_modified, Q.index_po, SUM(D.amount) 
                                                         AS total
                               FROM             dbo.QUOTATION AS Q WITH (nolock) INNER JOIN
                                                         dbo.QUOTATIONDETAIL AS D WITH (nolock) ON Q.idexport = D.idexport
                               GROUP BY  Q.dateimport, Q.idcustomer, Q.IDEMP, Q.idexport, Q.invoice, Q.idstatusquotation, Q.idquotationtype, Q.quotationno, Q.invoiceeps, Q.datepo, Q.idemppo, Q.idpotype, Q.last_modified, Q.index_po) AS Q ON 
                         EM.IDEMP = Q.idemppo LEFT OUTER JOIN
                         dbo.EMPCUS AS EC WITH (nolock) ON EM.IDEMP = EC.idemp AND EC.status = 'true' LEFT OUTER JOIN
                         dbo.DMCUSTOMERS AS C WITH (nolock) ON EC.idcustomer = C.idcustomer LEFT OUTER JOIN
                         dbo.PLANCRM AS CC WITH (nolock) ON C.idcustomer = CC.idcustomer AND CC.idemp = EM.IDEMP
GROUP BY EM.IDEMP, EM.StaffName
HAVING       (COUNT(DISTINCT EC.idcustomer) > 0)

--1 Số KH của 1 nhân viên (bảng 1)

SELECT       E.IDEMP, E.StaffName, C.idcustomer, C.customer, C.tax, F.fieldname, C.address
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
                         dbo.EMPCUS AS EC WITH (nolock) ON E.IDEMP = EC.idemp INNER JOIN
                         dbo.DMCUSTOMERS AS C WITH (nolock) ON EC.idcustomer = C.idcustomer INNER JOIN
                         dbo.DMFIELDS AS F WITH (nolock) ON C.idfields = F.idfields
WHERE        (E.IDEMP = 'EMP000044')

--2 Tổng số KH mới (bảng 2)

SELECT       E.IDEMP, E.StaffName, 
sum(case when C.idgroup = 0 then 1 else 0 end)  cong_ty,
sum(case when C.idgroup = 1 then 1 else 0 end)  dai_ly,
sum(case when C.idgroup = 2 then 1 else 0 end)  khach_le,
sum(case when C.idgroup = 3 then 1 else 0 end)  tieu_cuc
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
                         dbo.EMPCUS AS EC WITH (nolock) ON E.IDEMP = EC.idemp INNER JOIN
                         dbo.DMCUSTOMERS AS C WITH (nolock) ON EC.idcustomer = C.idcustomer 

GROUP BY E.IDEMP, E.StaffName
HAVING  SUM(CASE WHEN (EC.date1 > dateadd(day, - 30, getDate()) AND EC.status = 'true') THEN 1 ELSE 0 END)>0

--3 KH đã chăm sóc (bảng 3)
SELECT       E.IDEMP, E.StaffName, C.idcustomer, C.customer, isnull(sum(DI.quantity),0) total_qty_divice, isnull(sum(VI.quantity),0) total_qty_voxe
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
                         dbo.EMPCUS AS EC WITH (nolock) ON E.IDEMP = EC.idemp INNER JOIN
                         dbo.DMCUSTOMERS AS C WITH (nolock) ON EC.idcustomer = C.idcustomer 
						 LEFT JOIN dbo.DEVICEINFO AS DI WITH (nolock) ON C.idcustomer = DI.idcustomer
						 LEFT JOIN dbo.VOXEINFO AS VI WITH (nolock) ON C.idcustomer = VI.idcustomer
WHERE        (E.IDEMP = 'EMP000044')  
GROUP BY  E.IDEMP, E.StaffName, C.idcustomer, C.customer
HAVING sum(DI.quantity) >0 or sum(VI.quantity) >0

--4 KH Chưa chăm sóc (Bảng 4)
SELECT       E.IDEMP, E.StaffName, C.idcustomer, C.customer, C.tax, F.fieldname, C.address
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
                         dbo.EMPCUS AS EC WITH (nolock) ON E.IDEMP = EC.idemp INNER JOIN
                         dbo.DMCUSTOMERS AS C WITH (nolock) ON EC.idcustomer = C.idcustomer INNER JOIN
                         dbo.DMFIELDS AS F WITH (nolock) ON C.idfields = F.idfields
						 LEFT JOIN DEVICEINFO DI with(nolock) ON DI.idcustomer = EC.idcustomer
						 LEFT JOIN VOXEINFO VI with(nolock) ON VI.idcustomer = EC.idcustomer
						 LEFT JOIN BINHDIENINFO BI with(nolock) ON BI.idcustomer = EC.idcustomer
						 LEFT JOIN PLANCRM CC with(nolock) ON CC.idcustomer = EC.idcustomer


WHERE        (E.IDEMP = 'EMP000044')  and DI.idcustomer is  null and VI.idcustomer is  null and BI.idcustomer is  null and CC.idcustomer is  null

-- 5 KH có nhu cầu (bảng 5)

SELECT       E.IDEMP, E.StaffName, sum( case when E.iddepartment='BP000003' then 1 else 0 end ) po_cnt_sale, 
sum( case when E.iddepartment <>'BP000003' then 1 else 0 end ) po_cnt_other
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 dbo.QUOTATION AS Q WITH (nolock) ON E.IDEMP = Q.idemppo
where Q.idstatusquotation IN ('ST000006','ST000007','ST000008')
group by   E.IDEMP, E.StaffName

--Nhu cầu cơ hội (NC-CH) (bảng 6)

SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, '' Nguon, Q.dateimport ngay_tao,	QT.quotationtype,   
isnull( datediff(hour, getDate(), dateadd(hour, isnull(Q.duration,0), Q.quotation_term_date )),0) thoi_gian_con_lai , nguon_bg, nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 dbo.QUOTATION AS Q WITH (nolock) ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000006')


--Nhu cầu tiềm năng (NC-TN) (bảng 7)


SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, '' Nguon, Q.dateimport ngay_tao,	QT.quotationtype,   
isnull( datediff(hour, getDate(), dateadd(hour, isnull(Q.duration,0), Q.quotation_term_date )),0) thoi_gian_con_lai , nguon_bg, nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 dbo.QUOTATION AS Q WITH (nolock) ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000007')

--Nhu cầu không tiềm năng (NC-KTN) (bảng 8)

SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, '' Nguon, Q.dateimport ngay_tao,	QT.quotationtype,   
 nguon_bg, nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 dbo.QUOTATION AS Q WITH (nolock) ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000008')

--Hỏi giá (bảng 9)

SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, 	QT.quotationtype,   
isnull( datediff(hour, getDate(), dateadd(hour, isnull(Q.duration,0), Q.quotation_term_date )),0) thoi_gian_con_lai ,nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 dbo.QUOTATION AS Q WITH (nolock) ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000001')

--Báo giá (bảng 10)

SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, 	QT.quotationtype,   
isnull( datediff(hour, getDate(), dateadd(hour, isnull(Q.duration,0), Q.quotation_term_date )),0) thoi_gian_con_lai, (total) ,nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 ( SELECT Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep,Q.quotation_term_date,
 Q.nguon_bg, Q.duration, Q.reason, q.idemppo, Q.idcustomer,Q.idstatusquotation, sum(d.quantity*price) total FROM dbo.QUOTATION  Q WITH (nolock) inner join QUOTATIONDETAIL D  WITH (nolock) 
 ON Q.idexport = D.idexport group by Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep,Q.quotation_term_date,Q.nguon_bg, Q.duration, q.reason, Q.idemppo, Q.idcustomer, Q.idstatusquotation) AS Q ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000002')

--PO (bảng 11)
SELECT       E.IDEMP, E.StaffName, Q.quotationno, Q.invoiceeps, C.customer, 	QT.quotationtype, Q.datepo, Q.ngaydukien ,  
isnull( datediff(hour, getDate(), dateadd(hour, isnull(Q.duration,0), Q.quotation_term_date )),0) thoi_gian_con_lai, (total) ,nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 ( SELECT Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep, q.invoiceeps,Q.quotation_term_date,
 Q.nguon_bg, Q.duration, Q.reason, q.idemppo, Q.idcustomer,Q.idstatusquotation, Q.datepo, Q.ngaydukien, sum(d.quantity*price) total FROM dbo.QUOTATION  Q WITH (nolock) inner join QUOTATIONDETAIL D  WITH (nolock) 
 ON Q.idexport = D.idexport group by Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep,Q.quotation_term_date,
 Q.nguon_bg, q.invoiceeps, Q.duration, q.reason, Q.idemppo, Q.idcustomer, Q.idstatusquotation, Q.datepo, Q.ngaydukien) AS Q ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000004')

--Thất bại (bảng 12)
SELECT       E.IDEMP, E.StaffName, Q.quotationno, C.customer, 	QT.quotationtype,   
nguoi_can_thiep, Q.reason
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
 ( SELECT Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep,Q.quotation_term_date,
 Q.nguon_bg, Q.duration, Q.reason, q.idemppo, Q.idcustomer,Q.idstatusquotation, sum(d.quantity*price) total FROM dbo.QUOTATION  Q WITH (nolock) inner join QUOTATIONDETAIL D  WITH (nolock) 
 ON Q.idexport = D.idexport group by Q.quotationno,Q.idquotationtype,Q.nguoi_can_thiep,Q.quotation_term_date,Q.nguon_bg, Q.duration, q.reason, Q.idemppo, Q.idcustomer, Q.idstatusquotation) AS Q ON E.IDEMP = Q.idemppo
 inner join DMCUSTOMERS C with(nolock) ON C.idcustomer = Q.idcustomer
 INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON Q.idquotationtype = QT.idquotationtype
where Q.idstatusquotation IN ('ST000005')

--Loại nhu cầu
SELECT       E.IDEMP, E.StaffName ,
sum(D.quantity) total_qty, Q.idquotationtype, QT.quotationtype

FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
QUOTATION Q with(nolock) ON E.IDEMP = Q.idemppo 
INNER JOIN QUOTATIONDETAIL D with(nolock) 
ON Q.idexport = D.idexport
INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON qt.idquotationtype = Q.idquotationtype
GROUP BY E.IDEMP, E.StaffName ,
Q.idquotationtype, QT.quotationtype


-- chi tiết báo giá vs doanh sô

SELECT     D.idgrouptk ,GTK.grouptk,

sum( case when Q.idstatusquotation='ST000003' then D.quantity* D.price else 0 end) + sum( case when Q.idstatusquotation='ST000004' then D.quantity* D.price else 0 end) bao_gia
,  sum( case when Q.idstatusquotation='ST000004' then D.quantity* D.price else 0 end) doanh_so
FROM            dbo.EMPLOYEES AS E WITH (nolock) INNER JOIN
QUOTATION Q with(nolock) ON E.IDEMP = Q.idemppo 
INNER JOIN QUOTATIONDETAIL D with(nolock) 
ON Q.idexport = D.idexport
INNER JOIN DMQUOTATIONTYPE QT with(nolock) ON qt.idquotationtype = Q.idquotationtype
INNER JOIN DMGROUPTK GTK with(nolock) ON GTK.idgrouptk = D.idgrouptk
GROUP BY 
 D.idgrouptk ,GTK.grouptk--, E.IDEMP, e.StaffName