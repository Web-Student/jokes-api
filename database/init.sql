CREATE SCHEMA IF NOT EXISTS public;

CREATE TABLE public.joke(
    Id serial Not NULL primary key,
    Author character varying(100),
    Question character varying(500),
    Answer character varying(500)
);

INSERT INTO joke VALUES
(1, 'test@email.com', 'How do you make golden soup?', 'Add 24 carrots')
,(2, 'test@email.com', 'Dad, can you tell me what a solar eclipse is?', 'No son')
,(3, 'rachel.allen1@students.snow.edu', 'Have you ever eaten a clock?', 'It''s very time-consuming');


DO $$ BEGIN
  IF NOT EXISTS (SELECT 1 FROM pg_class WHERE relname = 'joke_id_seq') THEN
    CREATE SEQUENCE joke_id_seq START WITH 1 INCREMENT BY 1;
  END IF;
END $$;

-- CREATE SEQUENCE public.joke_id_seq
--     AS integer
--     START WITH 1
--     INCREMENT BY 1
--     NO MINVALUE
--     NO MAXVALUE
--     CACHE 1;

ALTER SEQUENCE public.joke_id_seq OWNED BY public.joke.Id;