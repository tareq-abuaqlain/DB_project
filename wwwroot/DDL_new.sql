
-- create schema
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.schemata
        WHERE schema_name = 'public'
    )
    THEN
        CREATE SCHEMA "public";
    END IF;
END;
$$;

-- public.department definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'department'
    )
    then
    	-- public.department definition

		-- Drop table
		
		-- DROP TABLE public.department;
		
		CREATE TABLE public.department (
			dept_name varchar(20) NOT NULL,
			building varchar(20) NULL,
			budget numeric(12,2) NULL,
			CONSTRAINT department_budget_check CHECK ((budget > (0)::numeric)),
			CONSTRAINT department_pkey PRIMARY KEY (dept_name)
		);
		
		-- Permissions
		
		ALTER TABLE public.department OWNER TO postgres;
		GRANT ALL ON TABLE public.department TO postgres;
    END IF;
END;
$$;


-- public.course definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'course'
    )
    then
    
    	-- public.course definition
		
		-- Drop table
		
		-- DROP TABLE public.course;
		
		CREATE TABLE public.course (
			course_id serial4 NOT NULL,
			title text NULL,
			dept_name varchar(20) NULL,
			credits numeric(2,0) NULL,
			CONSTRAINT course_credits_check CHECK ((credits > (0)::numeric)),
			CONSTRAINT course_pkey PRIMARY KEY (course_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.course OWNER TO postgres;
		GRANT ALL ON TABLE public.course TO postgres;
		
		
		-- public.course foreign keys
		
		ALTER TABLE public.course ADD CONSTRAINT course_dept_name_fkey FOREIGN KEY (dept_name) REFERENCES public.department(dept_name)ON DELETE SET NULL;
	END IF;
END;
$$;

-- public.instructor definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'instructor'
    )
    then
    
    	-- public.instructor definition

		-- Drop table
		
		-- DROP TABLE public.instructor;
		
		CREATE TABLE public.instructor (
			inst_id serial4 NOT NULL,
			"name" varchar(20) NULL,
			dept_name varchar(20) NULL,
			salary numeric(8, 2) NULL,
			CONSTRAINT instructor_pkey PRIMARY KEY (inst_id),
			CONSTRAINT instructor_salary_check CHECK ((salary > (29000)::numeric))
		);
		
		-- Permissions
		
		ALTER TABLE public.instructor OWNER TO postgres;
		GRANT ALL ON TABLE public.instructor TO postgres;
		
		
		-- public.instructor foreign keys
		
		ALTER TABLE public.instructor ADD CONSTRAINT instructor_dept_name_fkey FOREIGN KEY (dept_name) REFERENCES public.department(dept_name)ON DELETE SET NULL;
    END IF;
END;
$$;

-- public.student definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'student'
    )
    then
    
		-- public.student definition

		-- Drop table
		
		-- DROP TABLE public.student;

		CREATE TABLE public.student (
			std_id numeric(9) NOT NULL ,
			"name" varchar(30) NULL,
			dept_name varchar(20) NULL,
			place varchar(30) NULL,
			CONSTRAINT student_pkey PRIMARY KEY (std_id)
		);
		-- Permissions
		
		ALTER TABLE public.student OWNER TO postgres;
		GRANT ALL ON TABLE public.student TO postgres;	
	
  		-- "public".student foreign keys
		
		ALTER TABLE "public".student ADD CONSTRAINT student_dept_name_fkey FOREIGN KEY (dept_name) REFERENCES department(dept_name) ON DELETE SET NULL;
    END IF;
END;
$$;


-- public.std_phone definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'std_phone'
    )
    then
    	
    	-- public.std_phone definition

		-- Drop table
		
		-- DROP TABLE public.std_phone;
		
		CREATE TABLE public.std_phone (
			std_id int4 NOT NULL,
			phone numeric NOT NULL,
			CONSTRAINT std_phone_phone_check CHECK ((phone > (0)::numeric)),
			CONSTRAINT std_phone_pkey PRIMARY KEY (std_id, phone)
		);
		
		-- Permissions
		
		ALTER TABLE public.std_phone OWNER TO postgres;
		GRANT ALL ON TABLE public.std_phone TO postgres;
		
		
		-- public.std_phone foreign keys
		
		ALTER TABLE public.std_phone ADD CONSTRAINT std_phone_std_id_fkey FOREIGN KEY (std_id) REFERENCES public.student(std_id) ON DELETE SET NULL ;
    END IF;
END;
$$;

-- public.course_teach definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'course_teach'
    )
    then
    	-- public.course_teach definition

		-- Drop table
		
		-- DROP TABLE public.course_teach;
		
		CREATE TABLE public.course_teach (
			course_id int4 NOT NULL,
			inst_id int4 NOT NULL,
			topic varchar(60) NULL,
			book varchar(50) NULL,
			CONSTRAINT course_teach_pkey PRIMARY KEY (course_id, inst_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.course_teach OWNER TO postgres;
		GRANT ALL ON TABLE public.course_teach TO postgres;
		
		
		-- public.course_teach foreign keys
		
		ALTER TABLE public.course_teach ADD CONSTRAINT course_teach_course_id_fkey FOREIGN KEY (course_id) REFERENCES public.course(course_id)ON DELETE SET NULL;
		ALTER TABLE public.course_teach ADD CONSTRAINT course_teach_inst_id_fkey FOREIGN KEY (inst_id) REFERENCES public.instructor(inst_id)ON DELETE SET NULL;
    END IF;
END;
$$;

-- public.users definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'users'
    )
    then
	    -- public.users definition
		
		-- Drop table
		
		-- DROP TABLE public.users;
		
		CREATE TABLE public.users (
			user_id serial4 NOT NULL ,
			"name" varchar(20) NULL,
			"identity" numeric(9) NULL,
			"date" date NULL,
			username text NULL,
			"password" text NULL,
			CONSTRAINT users_pkey PRIMARY KEY (user_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.users OWNER TO postgres;
		GRANT ALL ON TABLE public.users TO postgres;
		insert into users ("name", "identity", "date", username, "password") 
		values ('Hamada',407069541,date '2001-3-13','hamada_0','IiRRLvRKYuWAuxwNyzOv9oj059qKSIrrTnykAsXKz0U=');
    END IF;
END;
$$;

-- public.user_phone definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'user_phone'
    )
    then
    	-- public.user_phone definition

		-- Drop table
		
		-- DROP TABLE public.user_phone;
		
		CREATE TABLE public.user_phone (
			user_id int4 NOT NULL,
			phone numeric NOT NULL,
			CONSTRAINT user_phone_pkey PRIMARY KEY (user_id, phone)
		);
		
		-- Permissions
		
		ALTER TABLE public.user_phone OWNER TO postgres;
		GRANT ALL ON TABLE public.user_phone TO postgres;
		
		
		-- public.user_phone foreign keys
		
		ALTER TABLE public.user_phone ADD CONSTRAINT user_phone_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id)ON DELETE SET NULL;
    END IF;
END;
$$;


-- public.roles definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'roles'
    )
    then
    	-- public.roles definition

		-- Drop table
		
		-- DROP TABLE public.roles;
		
		CREATE TABLE public.roles (
			role_id serial4 NOT NULL,
			"name" varchar(20) NULL,
			CONSTRAINT roles_pkey PRIMARY KEY (role_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.roles OWNER TO postgres;
		GRANT ALL ON TABLE public.roles TO postgres;
		insert into roles values (1,'admin');
		insert into roles values (2,'assistant');
    END IF;
END;
$$;




-- public.user_role definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'user_role'
    )
    then
    	-- public.user_role definition

		-- Drop table
		
		-- DROP TABLE public.user_role;
		
		CREATE TABLE public.user_role (
			user_id int4 NOT NULL,
			role_id int4 NOT NULL,
			CONSTRAINT user_role_pkey PRIMARY KEY (user_id, role_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.user_role OWNER TO postgres;
		GRANT ALL ON TABLE public.user_role TO postgres;
		
		
		-- public.user_role foreign keys
		
		ALTER TABLE public.user_role ADD CONSTRAINT user_role_role_id_fkey FOREIGN KEY (role_id) REFERENCES public.roles(role_id) ON DELETE SET NULL;
		ALTER TABLE public.user_role ADD CONSTRAINT user_role_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE SET NULL;
    	insert into user_role values (1 , 1);
	END IF;
END;
$$;

-- public.div definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'div'
    )
    then
    	-- public.div definition

		-- Drop table
		
		-- DROP TABLE public.div;
		
		CREATE TABLE public.div (
			div_id serial4 NOT NULL,
			course_id int4 NULL,
			inst_id int4 NULL,
			user_id int4 NULL,
			room int4 NULL,
			"day" int4 NULL,
			"time" time NULL,
			"type" int4 NULL,
			CONSTRAINT div_pkey PRIMARY KEY (div_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.div OWNER TO postgres;
		GRANT ALL ON TABLE public.div TO postgres;
		
		
		-- public.div foreign keys
		
		ALTER TABLE public.div ADD CONSTRAINT div_course_id_fkey FOREIGN KEY (course_id) REFERENCES public.course(course_id) ON DELETE SET NULL;
		ALTER TABLE public.div ADD CONSTRAINT div_inst_id_fkey FOREIGN KEY (inst_id) REFERENCES public.instructor(inst_id)ON DELETE SET NULL;
		ALTER TABLE public.div ADD CONSTRAINT div_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(user_id) ON DELETE SET NULL;
    END IF;
END;
$$;


-- public.registration definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'registration'
    )
    then
    	-- public.registration definition

		-- Drop table
		
		-- DROP TABLE public.registration;
		
		CREATE TABLE public.registration (
			std_id int4 NOT NULL,
			course_id int4 NOT NULL,
			div_id int4 NULL,
			"date" timestamp NULL,
			CONSTRAINT registration_pkey PRIMARY KEY (std_id, course_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.registration OWNER TO postgres;
		GRANT ALL ON TABLE public.registration TO postgres;
		
		
		-- public.registration foreign keys
		
		ALTER TABLE public.registration ADD CONSTRAINT registration_course_id_fkey FOREIGN KEY (course_id) REFERENCES public.course(course_id) ON DELETE SET NULL;
		ALTER TABLE public.registration ADD CONSTRAINT registration_div_id_fkey FOREIGN KEY (div_id) REFERENCES public.div(div_id) ON DELETE SET NULL;
		ALTER TABLE public.registration ADD CONSTRAINT registration_std_id_fkey FOREIGN KEY (std_id) REFERENCES public.student(std_id) ON DELETE SET NULL;
    END IF;
END;
$$;


-- public.lecture definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'lecture'
    )
    then
    	-- public.lecture definition

		-- Drop table
		
		-- DROP TABLE public.lecture;
		
		CREATE TABLE public.lecture (
			lecture_id serial4 NOT NULL,
			div_id int4 NULL,
			"time" timestamp NULL,
			title varchar(30) NULL,
			CONSTRAINT lecture_pkey PRIMARY KEY (lecture_id)
		);
		
		-- Permissions
		
		ALTER TABLE public.lecture OWNER TO postgres;
		GRANT ALL ON TABLE public.lecture TO postgres;
		
		
		-- public.lecture foreign keys
		
		ALTER TABLE public.lecture ADD CONSTRAINT lecture_div_id_fkey FOREIGN KEY (div_id) REFERENCES public.div(div_id)ON DELETE SET NULL;
    END IF;
END;
$$;




-- public.attend definition
DO
$$
BEGIN
    IF NOT EXISTS (
        SELECT 1
        FROM information_schema.tables
        WHERE table_schema = 'public'
        AND table_name = 'attend'
    )
    then
    	-- public.attend definition

		-- Drop table
		
		-- DROP TABLE public.attend;
		
		CREATE TABLE public.attend (
			lecture_id int4 NOT NULL,
			std_id int4 NOT NULL,
			isAttend bool not null,
			CONSTRAINT attend_pkey PRIMARY KEY (lecture_id, std_id)
		);
		-- Permissions
		
		ALTER TABLE public.attend OWNER TO postgres;
		GRANT ALL ON TABLE public.attend TO postgres;
		
		
		-- public.attend foreign keys
		
		ALTER TABLE public.attend ADD CONSTRAINT attend_lecture_id_fkey FOREIGN KEY (lecture_id) REFERENCES public.lecture(lecture_id)ON DELETE SET NULL;
		ALTER TABLE public.attend ADD CONSTRAINT attend_std_id_fkey FOREIGN KEY (std_id) REFERENCES public.student(std_id) ON DELETE SET NULL;
    END IF;
END;
$$;

















