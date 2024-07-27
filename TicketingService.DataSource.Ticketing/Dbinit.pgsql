-- PostgreSQL script

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS public."TicketStatus"
(
    "Id" uuid DEFAULT uuid_generate_v4() NOT NULL,
    "Value" varchar(50) COLLATE pg_catalog."default",
    "Order" smallint NOT NULL,
    CONSTRAINT "PK_TicketStatus" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

CREATE TABLE IF NOT EXISTS public."Ticket"
(
    "Id" uuid DEFAULT uuid_generate_v4() NOT NULL,
    "Description" varchar(500) COLLATE pg_catalog."default",
    "CustomerId" uuid NOT NULL,
    "TicketStatusId" uuid NOT NULL,
    "MetaDateCreated" timestamp with time zone NOT NULL,
    "MetaCreatedBy" varchar(20) COLLATE pg_catalog."default" NOT NULL,
    "MetaDateUpdated" timestamp with time zone,
    "MetaUpdatedBy" varchar(20) COLLATE pg_catalog."default",
    CONSTRAINT "PK_Ticket" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Ticket_TicketStatus_TicketStatusId" FOREIGN KEY ("TicketStatusId")
        REFERENCES public."TicketStatus" ("Id")
)

TABLESPACE pg_default;

DO $$
DECLARE
    id UUID;
BEGIN
    id := uuid_generate_v4();
    
    RAISE NOTICE 'Generated UUID: %', id;

    INSERT INTO public."TicketStatus" ("Value", "Order") VALUES
    ('Open', 1),
    ('In Progress', 2),
    ('Resolved', 3),
    ('Closed', 4),
    ('Pending', 5);

    -- Insert ticket data
    WITH StatusIds AS (
        SELECT
            "Id" AS status_id,
            "Value"
        FROM public."TicketStatus"
    ),
    InsertData AS (
        SELECT
            'System outage in production environment' AS description,
            id AS customer_id,
            (SELECT status_id FROM StatusIds WHERE "Value" = 'Open') AS ticket_status_id,
            '2024-07-20 10:30:00+00'::timestamptz AS meta_date_created,
            'admin' AS meta_created_by
        UNION ALL
        SELECT
            'Feature request for new dashboard' AS description,
            id AS customer_id,
            (SELECT status_id FROM StatusIds WHERE "Value" = 'In Progress') AS ticket_status_id,
            '2024-07-21 10:30:00+00'::timestamptz AS meta_date_created,
            'admin' AS meta_created_by
        UNION ALL
        SELECT
            'Bug report: application crashes on login' AS description,
            id AS customer_id,
            (SELECT status_id FROM StatusIds WHERE "Value" = 'Resolved') AS ticket_status_id,
            '2024-07-22 10:30:00+00'::timestamptz AS meta_date_created,
            'admin' AS meta_created_by
        UNION ALL
        SELECT
            'Request for API documentation update' AS description,
            id AS customer_id,
            (SELECT status_id FROM StatusIds WHERE "Value" = 'Closed') AS ticket_status_id,
            '2024-07-23 10:30:00+00'::timestamptz AS meta_date_created,
            'admin' AS meta_created_by
        UNION ALL
        SELECT
            'Database migration issue' AS description,
            id AS customer_id, -- Example CustomerId
            (SELECT status_id FROM StatusIds WHERE "Value" = 'Pending') AS ticket_status_id,
            '2024-07-24 10:30:00+00'::timestamptz AS meta_date_created,
            'admin' AS meta_created_by
    )
    INSERT INTO public."Ticket" (
        "Description", "CustomerId", "TicketStatusId", "MetaDateCreated", "MetaCreatedBy"
    )
    SELECT
        description, customer_id, ticket_status_id, meta_date_created, meta_created_by
    FROM InsertData;
END $$;

CREATE OR REPLACE FUNCTION public.get_all_tickets(
    p_sort_by varchar DEFAULT 'Id',
    p_sort_order varchar DEFAULT 'ASC',
    p_page_size int DEFAULT 10,
    p_page_number int DEFAULT 1
)
RETURNS TABLE (
    Id uuid,
    Description varchar(500),
    CustomerId uuid,
    MetaDateCreated timestamp with time zone,
    MetaCreatedBy varchar(20),
    MetaDateUpdated timestamp with time zone,
    MetaUpdatedBy varchar(20),
	TicketStatusId uuid,
	Value varchar(50)
) AS
$$
DECLARE
    v_offset int;
    v_sql text;
BEGIN
    v_offset := (p_page_number - 1) * p_page_size;

    v_sql := format(
        'SELECT
            t."Id",
            "Description",
            "CustomerId",
            "MetaDateCreated",
            "MetaCreatedBy",
            "MetaDateUpdated",
            "MetaUpdatedBy",
			"TicketStatusId",
			ts."Value"
        FROM public."Ticket" t
		INNER JOIN public."TicketStatus" ts 
		ON t."TicketStatusId" = ts."Id"
        ORDER BY %I %s
        LIMIT %s
        OFFSET %s',
        p_sort_by,
        p_sort_order,
        p_page_size,
        v_offset
    );

    RETURN QUERY EXECUTE v_sql;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION public.get_all_tickets_count()
RETURNS int AS
$$
DECLARE
    total_count int;
BEGIN

    SELECT COUNT(*) INTO total_count FROM public."Ticket";
    RETURN total_count;

END;
$$ LANGUAGE plpgsql;