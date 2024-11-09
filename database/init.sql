CREATE SCHEMA public;
--one more comment
-- i left out some of the heap stuff, etc in the inventory management api. if this fails bring that back in --
--in fact, here it is but commented out --
-- SET statement_timeout = 0;
-- SET lock_timeout = 0;
-- SET idle_in_transaction_session_timeout = 0;
-- SET client_encoding = 'UTF8';
-- SET standard_conforming_strings = on;
-- SELECT pg_catalog.set_config('search_path', '', false);
-- SET check_function_bodies = false;
-- SET xmloption = content;
-- SET client_min_messages = warning;
-- SET row_security = off;

-- COMMENT ON SCHEMA public IS 'standard public schema';
-- SET default_tablespace = '';
-- SET default_table_access_method = heap;

CREATE TABLE public.joke(
    Id integer Not NULL primary key,
    Author character varying(100),
    Question character varying(500),
    Answer character varying(500)
);

insert into table joke values
(1, "test@email.com", "How do you make golden soup?", "Add 24 carrots")
,(2, "test@email.com", "Dad, can you tell me what a solar eclipse is?", "No son")
,(3, "rachel.allen1@students.snow.edu", "Have you ever eaten a clock?", "It's very time-consuming")

CREATE SEQUENCE public.joke_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

ALTER SEQUENCE public.joke_id_seq OWNED BY public.joke.Id;