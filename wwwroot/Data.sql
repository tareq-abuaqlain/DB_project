--department

-- Insert data for Computer Science
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Computer Science', 'Engineering', 500000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Computer Science'
);

-- Insert data for Mathematics
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Mathematics', 'Science', 300000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Mathematics'
);

-- Insert data for Physics
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Physics', 'Science', 250000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Physics'
);

-- Insert data for Chemistry
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Chemistry', 'Science', 200000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Chemistry'
);

-- Insert data for Biology
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Biology', 'Life Sciences', 350000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Biology'
);

-- Insert data for Business 
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Business', 'Business School', 400000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Business'
);

-- Insert data for History
INSERT INTO public.department (dept_name, building, budget)
SELECT 'History', 'Humanities', 150000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'History'
);

-- Insert data for English
INSERT INTO public.department (dept_name, building, budget)
SELECT 'English', 'Humanities', 180000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'English'
);

-- Insert data for Psychology
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Psychology', 'Social Sciences', 280000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Psychology'
);

-- Insert data for Sociology
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Sociology', 'Social Sciences', 220000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Sociology'
);

-- Insert data for Art
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Art', 'Fine Arts', 200000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Art'
);

-- Insert data for Music
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Music', 'Fine Arts', 250000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Music'
);

-- Insert data for Engineering
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Engineering', 'Engineering', 450000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Engineering'
);

-- Insert data for Architecture
INSERT INTO public.department (dept_name, building, budget)
SELECT 'Architecture', 'Architecture', 400000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.department WHERE dept_name = 'Architecture'
);


--course

-- Insert data for Course 1
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Introduction to Computer Science', 'Computer Science', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Introduction to Computer Science'
);

-- Insert data for Course 2
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Calculus I', 'Mathematics', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Calculus I'
);

-- Insert data for Course 3
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Physics for Scientists and Engineers', 'Physics', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Physics for Scientists and Engineers'
);

-- Insert data for Course 4
INSERT INTO public.course (title, dept_name, credits)
SELECT 'General Chemistry', 'Chemistry', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'General Chemistry'
);

-- Insert data for Course 5
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Introduction to Biology', 'Biology', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Introduction to Biology'
);

-- Insert data for Course 6
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Principles of Business', 'Business', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Principles of Business'
);

-- Insert data for Course 7
INSERT INTO public.course (title, dept_name, credits)
SELECT 'World History', 'History', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'World History'
);

-- Insert data for Course 8
INSERT INTO public.course (title, dept_name, credits)
SELECT 'English Literature', 'English', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'English Literature'
);

-- Insert data for Course 9
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Introduction to Psychology', 'Psychology', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Introduction to Psychology'
);

-- Insert data for Course 10
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Sociology of Culture', 'Sociology', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Sociology of Culture'
);

-- Insert data for Course 11
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Introduction to Art', 'Art', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Introduction to Art'
);

-- Insert data for Course 12
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Music Theory', 'Music', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Music Theory'
);

-- Insert data for Course 13
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Introduction to Engineering', 'Engineering', 3
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Introduction to Engineering'
);

-- Insert data for Course 14
INSERT INTO public.course (title, dept_name, credits)
SELECT 'Architectural Design', 'Architecture', 4
WHERE NOT EXISTS (
    SELECT 1 FROM public.course WHERE title = 'Architectural Design'
);


-- instructors

-- Insert data for instructor 1
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'John Doe', 'Computer Science', 50000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'John Doe'
);

-- Insert data for instructor 2
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Jane Smith', 'Mathematics', 45000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Jane Smith'
);

-- Insert data for instructor 3
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'David Johnson', 'Physics', 52000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'David Johnson'
);

-- Insert data for instructor 4
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Jennifer Davis', 'Chemistry', 48000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Jennifer Davis'
);

-- Insert data for instructor 5
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Michael Wilson', 'Biology', 55000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Michael Wilson'
);

-- Insert data for instructor 6
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Emily Anderson', 'Business', 52000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Emily Anderson'
);

-- Insert data for instructor 7
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Robert Thompson', 'History', 42000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Robert Thompson'
);

-- Insert data for instructor 8
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Sophia Martinez', 'English', 39000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Sophia Martinez'
);

-- Insert data for instructor 9
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'William Hernandez', 'Psychology', 48000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'William Hernandez'
);

-- Insert data for instructor 10
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Olivia Lopez', 'Sociology', 45000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Olivia Lopez'
);

-- Insert data for instructor 11
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Daniel Gonzalez', 'Art', 41000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Daniel Gonzalez'
);

-- Insert data for instructor 12
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Mia Miller', 'Music', 47000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Mia Miller'
);

-- Insert data for instructor 13
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Christopher Wilson', 'Engineering', 52000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Christopher Wilson'
);

-- Insert data for instructor 14
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Elizabeth Anderson', 'Architecture', 51000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Elizabeth Anderson'
);

-- Insert data for instructor 15
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Andrew Lee', 'Computer Science', 48000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Andrew Lee'
);

-- Insert data for instructor 16
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Abigail Taylor', 'Mathematics', 45000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Abigail Taylor'
);

-- Insert data for instructor 17
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'James Moore', 'Physics', 52000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'James Moore'
);

-- Insert data for instructor 18
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Charlotte Harris', 'Chemistry', 48000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Charlotte Harris'
);

-- Insert data for instructor 19
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Benjamin Clark', 'Biology', 54000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Benjamin Clark'
);

-- Insert data for instructor 20
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Ava Young', 'Business', 52000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Ava Young'
);

-- Insert data for instructor 21
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Joseph Turner', 'History', 43000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Joseph Turner'
);

-- Insert data for instructor 22
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Harper Martin', 'English', 40000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Harper Martin'
);

-- Insert data for instructor 23
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Samuel Young', 'Psychology', 49000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Samuel Young'
);

-- Insert data for instructor 24
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Victoria Allen', 'Sociology', 46000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Victoria Allen'
);

-- Insert data for instructor 25
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Matthew Nelson', 'Art', 42000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Matthew Nelson'
);

-- Insert data for instructor 26
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Lillian Thompson', 'Music', 48000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Lillian Thompson'
);

-- Insert data for instructor 27
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Daniel Turner', 'Engineering', 53000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Daniel Turner'
);

-- Insert data for instructor 28
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Sofia Hill', 'Architecture', 49000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Sofia Hill'
);

-- Insert data for instructor 29
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Ethan Murphy', 'Computer Science', 47000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Ethan Murphy'
);

-- Insert data for instructor 30
INSERT INTO public.instructor (name, dept_name, salary)
SELECT 'Avery Adams', 'Mathematics', 45000.00
WHERE NOT EXISTS (
    SELECT 1 FROM public.instructor WHERE name = 'Avery Adams'
);



-- students

-- Insert data into public.student table if not found
INSERT INTO public.student (std_id, "name", dept_name, place)
SELECT * FROM (
    VALUES
    (120190675, 'John Doe', 'Computer Science', 'New York'),
    (120190676, 'Jane Smith', 'Mathematics', 'Los Angeles'),
    (120190677, 'Michael Johnson', 'Physics', 'Chicago'),
    (120190678, 'Sarah Adams', 'Chemistry', 'Houston'),
    (120190679, 'Robert Williams', 'Biology', 'Miami'),
    (120190680, 'Emily Davis', 'Business', 'Dallas'),
    (120190681, 'Daniel Brown', 'History', 'Boston'),
    (120190682, 'Olivia Wilson', 'English', 'Seattle'),
    (120190683, 'Matthew Clark', 'Psychology', 'San Francisco'),
    (120190684, 'Ava Garcia', 'Sociology', 'Los Angeles'),
    (120190685, 'Joseph Martinez', 'Art', 'New York'),
    (120190686, 'Sophia Robinson', 'Music', 'Chicago'),
    (120190687, 'William Hall', 'Engineering', 'Houston'),
    (120190688, 'Isabella Lewis', 'Architecture', 'Miami'),
    (120190689, 'James Wright', 'Computer Science', 'Dallas'),
    (220190690, 'Grace Hill', 'Mathematics', 'Boston'),
    (220190691, 'Logan Turner', 'Physics', 'Seattle'),
    (220190692, 'Chloe Adams', 'Chemistry', 'San Francisco'),
    (220190693, 'Henry White', 'Biology', 'Los Angeles'),
    (220190694, 'Lily Moore', 'Business', 'New York'),
    (220190695, 'Jackson Mitchell', 'History', 'Chicago'),
    (220190696, 'Sophia Johnson', 'English', 'Houston'),
    (220190697, 'Elijah Davis', 'Psychology', 'Miami'),
    (220190698, 'Avery Anderson', 'Sociology', 'Dallas'),
    (220190699, 'Amelia Martinez', 'Art', 'Boston'),
    (220190700, 'Samuel Thompson', 'Music', 'Seattle'),
    (220190701, 'Scarlett Harris', 'Engineering', 'San Francisco'),
    (220190702, 'Noah Turner', 'Architecture', 'Los Angeles'),
    (220190703, 'Mia Hall', 'Computer Science', 'New York'),
    (220190704, 'Ethan Lewis', 'Mathematics', 'Chicago'),
    (120190705, 'Oliver Walker', 'Chemistry', 'Houston'),
    (120190706, 'Emma Foster', 'Biology', 'Miami'),
    (120190707, 'Sebastian Butler', 'Business', 'Dallas'),
    (120190708, 'Mia Simmons', 'History', 'Boston'),
    (120190709, 'Elijah Cook', 'English', 'Seattle'),
    (120190710, 'Avery Rivera', 'Psychology', 'San Francisco'),
    (120190711, 'Scarlett Cooper', 'Sociology', 'Los Angeles'),
    (120190712, 'Lucas Ward', 'Art', 'New York'),
    (120190713, 'Abigail Brooks', 'Music', 'Chicago'),
    (120190714, 'Henry Foster', 'Engineering', 'Houston'),
    (120190715, 'Sophia Reed', 'Architecture', 'Miami'),
    (120190716, 'Jackson Hayes', 'Computer Science', 'Dallas'),
    (220190717, 'Lily Coleman', 'Mathematics', 'Boston'),
    (220190718, 'Logan Henderson', 'Physics', 'Seattle'),
    (220190719, 'Chloe Price', 'Chemistry', 'San Francisco'),
    (220190720, 'Carter Bell', 'Biology', 'Los Angeles'),
    (220190721, 'Grace Peterson', 'Business', 'New York'),
    (220190722, 'Oliver Morgan', 'History', 'Chicago'),
    (220190723, 'Emily Ward', 'English', 'Houston'),
    (220190724, 'Daniel Turner', 'Psychology', 'Miami'),
    (220190725, 'Hannah Davis', 'Sociology', 'Dallas'),
    (220190726, 'Matthew Thompson', 'Art', 'Boston'),
    (220190727, 'Addison Reed', 'Music', 'Seattle'),
    (220190728, 'Ella Morris', 'Engineering', 'San Francisco'),
    (220190729, 'Andrew Cooper', 'Architecture', 'Los Angeles'),
    (220190730, 'Victoria Wright', 'Computer Science', 'New York'),
    (120190731, 'Leo Bryant', 'Mathematics', 'Chicago'),
    (120190732, 'Penelope Kelly', 'Physics', 'Houston'),
    (120190733, 'Jack Mitchell', 'Chemistry', 'Miami'),
    (120190734, 'Sophie Hughes', 'Biology', 'Dallas'),
    (120190735, 'Benjamin Ross', 'Business', 'Boston'),
    (120190736, 'Nora Ramirez', 'History', 'Seattle'),
    (120190737, 'Ethan Brooks', 'English', 'San Francisco'),
    (120190738, 'Aria Sanders', 'Psychology', 'Los Angeles'),
    (120190739, 'Caleb Richardson', 'Sociology', 'New York'),
    (120190740, 'Madison Simmons', 'Art', 'Chicago'),
    (120190741, 'Gabriel Wright', 'Music', 'Houston'),
    (120190742, 'Mila Parker', 'Engineering', 'Miami'),
    (120190743, 'Isaac Price', 'Architecture', 'Dallas'),
    (120190744, 'Evelyn Morris', 'Computer Science', 'Boston'),
    (220190745, 'Owen Bell', 'Mathematics', 'Seattle'),
    (220190746, 'Aurora Henderson', 'Physics', 'San Francisco'),
    (220190747, 'Wyatt Coleman', 'Chemistry', 'Los Angeles'),
    (220190748, 'Harper Peterson', 'Biology', 'New York'),
    (220190749, 'Grayson Morgan', 'Business', 'Chicago'),
    (220190750, 'Elizabeth Hayes', 'History', 'Houston'),
    (220190751, 'Landon Turner', 'English', 'Miami'),
    (220190752, 'Nova Ward', 'Psychology', 'Dallas'),
    (220190753, 'Sofia Davis', 'Sociology', 'Boston'),
    (220190754, 'Zachary Thompson', 'Art', 'Seattle'),
    (220190755, 'Stella Reed', 'Music', 'San Francisco')
) AS new_students
WHERE NOT EXISTS (
    SELECT 1 FROM public.student WHERE std_id = new_students.column1
);


-- std_phone

-- Generate random phone numbers
-- Manually insert data into public.std_phone table
INSERT INTO public.std_phone (std_id, phone)
SELECT column1 , column2::numeric  FROM (
VALUES
    (120190675, '0591234567'),
    (120190676, '0569876543'),
    (120190677, '0595554444'),
    (120190678, '0561112222'),
    (120190679, '0597778888'),
    (120190680, '0563332222'),
    (120190681, '0598887777'),
    (120190682, '0564445555'),
    (120190683, '0599990000'),
    (120190684, '0566667777'),
    (120190685, '0592223333'),
    (120190686, '0565556666'),
    (120190687, '0594445555'),
    (120190688, '0562223333'),
    (120190689, '0597778888'),
    (220190690, '0591112222'),
    (220190691, '0563334444'),
    (220190692, '0592223333'),
    (220190693, '0564445555'),
    (220190694, '0595556666'),
    (220190695, '0566667777'),
    (220190696, '0597778888'),
    (220190697, '0568889999'),
    (220190698, '0599990000'),
    (220190699, '0560001111'),
    (220190700, '0591112222'),
    (220190701, '0562223333'),
    (220190702, '0593334444'),
    (220190703, '0564445555'),
    (220190704, '0595556666'),
    (120190705, '0566667777'),
    (120190706, '0597778888'),
    (120190707, '0568889999'),
    (120190708, '0599990000'),
    (120190709, '0560001111'),
    (120190710, '0591112222'),
    (120190711, '0562223333'),
    (120190712, '0593334444'),
    (120190713, '0564445555'),
    (120190714, '0595556666'),
    (120190715, '0566667777'),
    (120190716, '0597778888'),
    (220190717, '0568889999'),
    (220190718, '0599990000'),
    (220190719, '0560001111'),
    (220190720, '0591112222'),
    (220190721, '0562223333'),
    (220190722, '0593334444'),
    (220190723, '0564445555'),
    (220190724, '0595556666'),
    (220190725, '0566667777'),
    (220190726, '0597778888'),
    (220190727, '0568889999'),
    (220190728, '0599990000'),
    (220190729, '0560001111'),
    (220190730, '0591112222'),
    (120190731, '0562223333'),
    (120190732, '0593334444'),
    (120190733, '0564445555'),
    (120190734, '0595556666'),
    (120190735, '0566667777'),
    (120190736, '0597778888'),
    (120190737, '0568889999'),
    (120190738, '0599990000'),
    (120190739, '0560001111'),
    (120190740, '0591112222'),
    (120190741, '0562223333'),
    (120190742, '0593334444'),
    (120190743, '0564445555'),
    (120190744, '0595556666'),
    (220190745, '0566667777'),
    (220190746, '0597778888'),
    (220190747, '0568889999'),
    (220190748, '0599990000'),
    (220190749, '0560001111'),
    (220190750, '0591112222'),
    (220190751, '0562223333'),
    (220190752, '0593334444'),
    (220190753, '0564445555'),
    (220190754, '0595556666'),
    (220190755, '0566667777')
) AS new_phones
WHERE NOT EXISTS (
    SELECT 1 FROM public.std_phone  WHERE std_id = new_phones .column1  and phone = new_phones .column2::numeric  
);


-- course_teach

INSERT INTO public.course_teach (course_id, inst_id, topic, book)
SELECT course_id, inst_id, topic, book
FROM (
    VALUES
        (6, 1, 'Introduction to Computer Science', 'Computer Science Illuminated'),
	    (7, 2, 'Data Structures and Algorithms', 'Introduction to Algorithms'),
	    (8, 3, 'Database Systems', 'Database Systems: The Complete Book'),
	    (9, 4, 'Machine Learning', 'Pattern Recognition and Machine Learning'),
	    (10, 5, 'Operating Systems', 'Operating System Concepts'),
	    (11, 6, 'Software Engineering', 'Software Engineering: A Practitioner''s Approach'),
	    (12, 7, 'Computer Networks', 'Computer Networking: A Top-Down Approach'),
	    (13, 8, 'Artificial Intelligence', 'Artificial Intelligence: A Modern Approach'),
	    (14, 9, 'Web Development', 'HTML and CSS: Design and Build Websites'),
	    (1, 10, 'Digital Logic Design', 'Digital Design and Computer Architecture'),
	    (2, 11, 'Computer Graphics', 'Computer Graphics: Principles and Practice'),
	    (3, 12, 'Data Mining', 'Data Mining: Concepts and Techniques'),
	    (4, 13, 'Cybersecurity', 'Principles of Computer Security'),
	    (5, 14, 'Software Testing', 'A Practical Guide to Software Testing'),
	    -- Add more rows here
	    (6, 15, 'Introduction to Computer Science', 'Computer Science Illuminated'),
	    (7, 16, 'Data Structures and Algorithms', 'Introduction to Algorithms'),
	    (8, 17, 'Database Systems', 'Database Systems: The Complete Book'),
	    (9, 18, 'Machine Learning', 'Pattern Recognition and Machine Learning'),
	    (10, 19, 'Operating Systems', 'Operating System Concepts'),
	    (11, 20, 'Software Engineering', 'Software Engineering: A Practitioner''s Approach'),
	    (12, 21, 'Computer Networks', 'Computer Networking: A Top-Down Approach'),
	    (13, 22, 'Artificial Intelligence', 'Artificial Intelligence: A Modern Approach'),
	    (14, 23, 'Web Development', 'HTML and CSS: Design and Build Websites'),
	    (1, 24, 'Digital Logic Design', 'Digital Design and Computer Architecture'),
	    (2, 25, 'Computer Graphics', 'Computer Graphics: Principles and Practice'),
	    (3, 26, 'Data Mining', 'Data Mining: Concepts and Techniques'),
	    (4, 27, 'Cybersecurity', 'Principles of Computer Security'),
	    (5, 28, 'Software Testing', 'A Practical Guide to Software Testing'),
	    -- Add more rows here
	    (6, 29, 'Introduction to Computer Science', 'Computer Science Illuminated'),
	    (7, 30, 'Data Structures and Algorithms', 'Introduction to Algorithms'),
	    (8, 1, 'Database Systems', 'Database Systems: The Complete Book'),
	    (9, 2, 'Machine Learning', 'Pattern Recognition and Machine Learning'),
	    (10, 3, 'Operating Systems', 'Operating System Concepts'),
	    (11, 4, 'Software Engineering', 'Software Engineering: A Practitioner''s Approach'),
	    (12, 5, 'Computer Networks', 'Computer Networking: A Top-Down Approach'),
	    (13, 6, 'Artificial Intelligence', 'Artificial Intelligence: A Modern Approach'),
	    (14, 7, 'Web Development', 'HTML and CSS: Design and Build Websites'),
	    (1, 8, 'Digital Logic Design', 'Digital Design and Computer Architecture'),
	    (2, 9, 'Computer Graphics', 'Computer Graphics: Principles and Practice'),
	    (3, 10, 'Data Mining', 'Data Mining: Concepts and Techniques'),
	    (4, 11, 'Cybersecurity', 'Principles of Computer Security'),
	    (5, 12, 'Software Testing', 'A Practical Guide to Software Testing')
        -- Add more rows here
) AS data (course_id, inst_id, topic, book)
WHERE NOT EXISTS (
    SELECT 1 FROM public.course_teach WHERE course_id = data.course_id AND inst_id = data.inst_id
);


-- users
--create EXTENSION IF NOT EXISTS pgcrypto;

-- Insert data into the users table if not found
INSERT INTO public.users ("name", "identity", "date", username, "password")
SELECT 'John Doe', 123456789, '2022-01-01', 'johndoe', 'CxTVAaWURCoBxoWVQbyz6BZNGD0yk3uFGDVEL2nVyU4='  --'password1'
WHERE NOT EXISTS (
    SELECT 1 FROM public.users WHERE username = 'johndoe'
);

INSERT INTO public.users ("name", "identity", "date", username, "password")
SELECT 'Jane Smith', 987654321, '2022-02-01', 'janesmith', 'bPYV1byqx3g1Ko8fM2DSPwLzTsGC4lmJf9bOSF14cNQ='--'password2'
WHERE NOT EXISTS (
    SELECT 1 FROM public.users WHERE username = 'janesmith'
);

INSERT INTO public.users ("name", "identity", "date", username, "password")
SELECT 'David Johnson', 543216789, '2022-03-01', 'davidjohnson', 'WQasNhoTfi0oZGXNZYjrtaw/WulVABEAvEFXfD11F2Q='--'password3'
WHERE NOT EXISTS (
    SELECT 1 FROM public.users WHERE username = 'davidjohnson'
);

INSERT INTO public.users ("name", "identity", "date", username, "password")
SELECT 'Sarah Williams', 987654321, '2022-04-01', 'sarahwilliams', 'uXhzpA9zq+3Y1oWnzV5fheSpz7g+rCaIZkCggThQEis='--'password4'
WHERE NOT EXISTS (
    SELECT 1 FROM public.users WHERE username = 'sarahwilliams'
);

INSERT INTO public.users ("name", "identity", "date", username, "password")
SELECT 'Michael Brown', 123459876, '2022-05-01', 'michaelbrown', 'iyyG6pzy6k61F/0eBrdPOZ5/7A/vkuO0gqbPLisJICM='--'password5'
WHERE NOT EXISTS (
    SELECT 1 FROM public.users WHERE username = 'michaelbrown'
);




-- user phone

-- Manually insert data into public.std_phone table
INSERT INTO public.user_phone  (user_id , phone)
SELECT column1 , column2::numeric  FROM (
VALUES
    (1, '0597778888'),
    (1, '0591112222'),
    (2, '0563334444'),
    (2, '0592223333'),
    (3, '0564445555'),
    (3, '0595556666'),
    (2, '0566667777'),
    (3, '0597778888'),
    (4, '0568889999'),
    (4, '0599990000'),
    (4, '0560001111'),
    (5, '0591112222'),
    (5, '0562223333'),
    (6, '0593334444'),
    (6, '0564445555'),
    (1, '0595556666'),
    (5, '0566667777'),
    (6, '0597778888')
) AS new_phones_user
WHERE NOT EXISTS (
    SELECT 1 FROM public.user_phone WHERE user_id = new_phones_user.column1  and phone = new_phones_user.column2::numeric  
);


-- user role
insert into user_role (user_id , role_id)
select * from (
	values 
		(2,2),
		(3,2),
		(4,2),
		(5,2),
		(6,2)
) as new_roles
where not exists (
	select 1 from user_role where user_id = new_roles.column1  and role_id = new_roles.column2 
);


































