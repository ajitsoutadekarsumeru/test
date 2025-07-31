WITH FeedbackStats AS (
    SELECT
        accountid,
        COUNT(id) AS total_feedbacks,
        SUM(CASE 
                WHEN MONTH(lastmodifieddate) = MONTH(CURRENT_DATE()) AND YEAR(lastmodifieddate) = YEAR(CURRENT_DATE()) 
                THEN 1 ELSE 0 
            END) AS feedbacks_this_month
    FROM feedback
    GROUP BY accountid
)

SELECT
    la.id AS id_loanaccounts,
    fbak.lastmodifieddate AS lastmodifieddate_loanaccounts,
    COALESCE(fbak.feedbackdate, '') AS fbak_feedbackdate,
    COALESCE(DATE_FORMAT(fbak.feedbackdate, '%Y-%m-%d'), '') AS fbak_feedbackdate_yyyymmdd,
    COALESCE(fbak.dispositioncode, '') AS fbak_dispositioncode,
    COALESCE(fbak.dispositiongroup, '') AS fbak_dispositiongroup,
    COALESCE(fbstats.feedbacks_this_month, 0) AS fbak_totalfeedbacksthismonth,
    COALESCE(fbstats.total_feedbacks, 0) AS fbak_totalfeedbackstilldate,
    CASE
        WHEN fbstats.feedbacks_this_month > 0 THEN 'Attempted'
        ELSE 'Unattempted'
    END AS feedback_status,
    fbak.lastmodifieddate AS fbak_lastmodifieddate,
    fbak.accountid AS fbak_accountid
	
FROM
    feedback AS fbak
LEFT JOIN
    loanaccounts AS la ON fbak.accountid = la.id
LEFT JOIN
    applicationuser AS feedbackcollector ON fbak.collectorid = feedbackcollector.id
LEFT JOIN
    FeedbackStats AS fbstats ON fbak.accountid = fbstats.accountid
WHERE
    fbak.lastmodifieddate > :sql_last_value
ORDER BY
    fbak.lastmodifieddate ASC
