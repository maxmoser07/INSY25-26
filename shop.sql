drop function if exists Mwst;
CREATE FUNCTION Mwst(net FLOAT, tax_class INT)
    RETURNS FLOAT -- float rundet automatisch auf 7 Nachkommastellen mit RETURNS
    DETERMINISTIC
BEGIN
    DECLARE rate FLOAT;
    DECLARE brutto FLOAT;

    SELECT t.rate INTO rate
    FROM taxes t
    WHERE t.tax_class = tax_class;

    SET brutto = net * (1 + rate/100);

    RETURN ROUND(brutto, 2);
END;