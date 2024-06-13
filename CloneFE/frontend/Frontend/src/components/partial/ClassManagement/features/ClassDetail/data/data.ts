export const courses = [
    {
        id: 1,
        syllabusName: "Linux",
        syllabusURL: "https://www.google.com",
        syllabusStatus: "Active",
        syllabusShortName: "LIN v2.0",
        duration: {
            days: 4,
            hours: 12,
        },
        createdAt: "2022-07-23",
        createdBy: "Ghostface",
        trainer: [
            {
                id: 1,
                name: "Jason Voorhees",
                profileURL: "https://storage.googleapis.com/pai-images/f378fefbf1ce45caad20e0f4b837e1b2.jpeg",
            },
            {
                id: 2,
                name: "Freddy Krueger",
                profileURL: "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/2c7edcba-acdb-4d5b-86f5-5bd12066173e/d2p0axj-99f7dda7-a8f7-4c75-8134-7068784efaf4.jpg/v1/fill/w_900,h_1333,q_75,strp/freddy_krueger_by_redcab_d2p0axj-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzJjN2VkY2JhLWFjZGItNGQ1Yi04NmY1LTViZDEyMDY2MTczZVwvZDJwMGF4ai05OWY3ZGRhNy1hOGY3LTRjNzUtODEzNC03MDY4Nzg0ZWZhZjQuanBnIiwiaGVpZ2h0IjoiPD0xMzMzIiwid2lkdGgiOiI8PTkwMCJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS53YXRlcm1hcmsiXSwid21rIjp7InBhdGgiOiJcL3dtXC8yYzdlZGNiYS1hY2RiLTRkNWItODZmNS01YmQxMjA2NjE3M2VcL3JlZGNhYi00LnBuZyIsIm9wYWNpdHkiOjk1LCJwcm9wb3J0aW9ucyI6MC40NSwiZ3Jhdml0eSI6ImNlbnRlciJ9fQ.zJG0HZnl0Q7vzVXE0KeTTgnmLRyxSUwatxP7cEXNg5o",
            },
            {
                id: 3,
                name: "Michael Myers",
                profileURL: "data:image/jpeg;base64,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",
            }
        ]
    },
    {
        id: 2,
        syllabusName: "AWS Basic",
        syllabusURL: "https://www.google.com",
        syllabusStatus: "Active",
        syllabusShortName: "AWS v1.0",
        duration: {
            days: 7,
            hours: 21,
        },
        createdAt: "2022-07-23",
        createdBy: "Jason Voorhees",
        trainer: [
            {
                id: 1,
                name: "Jason Voorhees",
                profileURL: "https://storage.googleapis.com/pai-images/f378fefbf1ce45caad20e0f4b837e1b2.jpeg",
            },
            {
                id: 2,
                name: "Freddy Krueger",
                profileURL: "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/2c7edcba-acdb-4d5b-86f5-5bd12066173e/d2p0axj-99f7dda7-a8f7-4c75-8134-7068784efaf4.jpg/v1/fill/w_900,h_1333,q_75,strp/freddy_krueger_by_redcab_d2p0axj-fullview.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7InBhdGgiOiJcL2ZcLzJjN2VkY2JhLWFjZGItNGQ1Yi04NmY1LTViZDEyMDY2MTczZVwvZDJwMGF4ai05OWY3ZGRhNy1hOGY3LTRjNzUtODEzNC03MDY4Nzg0ZWZhZjQuanBnIiwiaGVpZ2h0IjoiPD0xMzMzIiwid2lkdGgiOiI8PTkwMCJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS53YXRlcm1hcmsiXSwid21rIjp7InBhdGgiOiJcL3dtXC8yYzdlZGNiYS1hY2RiLTRkNWItODZmNS01YmQxMjA2NjE3M2VcL3JlZGNhYi00LnBuZyIsIm9wYWNpdHkiOjk1LCJwcm9wb3J0aW9ucyI6MC40NSwiZ3Jhdml0eSI6ImNlbnRlciJ9fQ.zJG0HZnl0Q7vzVXE0KeTTgnmLRyxSUwatxP7cEXNg5o",
            },
            {
                id: 3,
                name: "Michael Myers",
                profileURL: "data:image/jpeg;base64,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",
            }
        ]
    },
    {
        id: 3,
        syllabusName: "Docker",
        syllabusURL: "https://www.google.com",
        syllabusStatus: "Inactive",
        syllabusShortName: "DOC v1.5",
        duration: {
            days: 6,
            hours: 18,
        },
        createdAt: "2022-07-23",
        createdBy: "Ghostface",
        trainer: [
            {
                id: 3,
                name: "Michael Myers",
                profileURL: "data:image/jpeg;base64,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",
            },
            {
                id: 4,
                name: "Ghostface",
                profileURL: "https://i.pinimg.com/736x/cf/d1/bc/cfd1bc15f8b75790808dc64310c20f39.jpg",
            },
        ]
    },
];

export const learningDates = [
    // {
    //     id: 1,
    //     date: new Date('2024-02-11'),
    //     className: "Fresher Develop Operation",
    //     dateOrder: "Day 10 of 31",
    //     unitIndex: "Unit 6",
    //     unitTitle: "MVC Architecture in ASP.NET",
    //     location: "Ftown2",
    //     trainer: {
    //         id: 1,
    //         name: "Dinh Vu Quoc Trung",
    //         profileURL: "https://www.google.com"
    //     },
    //     admin: {
    //         id: 2,
    //         name: "Ly Lien Lien Dung",
    //         profileURL: "https://www.google.com"
    //     }
    // },
    // {
    //     id: 2,
    //     date: new Date('2024-02-13'),
    //     className: "Fresher Develop Operation",
    //     dateOrder: "Day 11 of 31",
    //     unitIndex: "Unit 6",
    //     unitTitle: "MVC Architecture in ASP.NET",
    //     location: "Ftown2",
    //     trainer: {
    //         id: 1,
    //         name: "Dinh Vu Quoc Trung",
    //         profileURL: "https://www.google.com"
    //     },
    //     admin: {
    //         id: 2,
    //         name: "Le Nguyen Gia Bao",
    //         profileURL: "https://www.facebook.com/GiaBaoo0512"
    //     }
    // }
]