CREATE TABLE IF NOT EXISTS general_type (
    gdtype TEXT NOT NULL,
    name_local TEXT,
    name_eng TEXT,
    status CHAR(1) DEFAULT 'A' CHECK (status IN ('A', 'I')),
    create_date TIMESTAMPTZ DEFAULT NOW(),
    create_user TEXT,
    modify_date TIMESTAMPTZ DEFAULT NOW(),
    modify_user TEXT,
    PRIMARY KEY (gdtype)
);

CREATE TABLE IF NOT EXISTS general_desc (
    id integer NOT NULL,
    gdtype TEXT NOT NULL,
    gdcode TEXT NOT NULL,
    desc1 TEXT,
    desc2 TEXT,
    desc3 TEXT,
    desc4 TEXT,
    desc5 TEXT,
    cond1 TEXT,
    cond2 TEXT,
    cond3 TEXT,
    cond4 TEXT,
    cond5 TEXT,
    status CHAR(1) DEFAULT 'A' CHECK (status IN ('A', 'I')),
    create_date TIMESTAMPTZ DEFAULT NOW(),
    create_user TEXT,
    modify_date TIMESTAMPTZ DEFAULT NOW(),
    modify_user TEXT,
    foreign key (gdtype) references general_type (gdtype) ON UPDATE CASCADE ON DELETE RESTRICT,
    PRIMARY KEY (id)
);