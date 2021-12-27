
DECLARE 
    ID NUMBER;
    rec_ID NUMBER;
    rec_PID NUMBER;
    rec_CA_VID_ID NUMBER;
    rec_CA_ACTIVE NUMBER;
    rec_NAME varchar2(50);
    rec_VALUE NUMBER;   
BEGIN

    FOR rec IN (SELECT 
    ID, PID, CA_VID_ID, CA_ACTIVE, name, value
    FROM CALCULATOR 
    WHERE CA_ACTIVE = 2)

LOOP

    DBMS_OUTPUT.put_line('Активный калькулятор: ID = ' || rec.ID ||
    ' PID = ' || rec.PID ||
    ' CA_VID_ID = ' || rec.ca_vid_id ||
    ' CA_ACTIVITE = ' || rec.ca_active ||
    ' NAME = ' || rec.NAME ||
    ' VALUE = ' || rec.value);
    
END LOOP;

END;
