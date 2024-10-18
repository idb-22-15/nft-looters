import type { UserProfile } from '~/src/shared/model/user'

import type { Certificate } from '../types'

const mockUsers: UserProfile[] = [
  {
    id: 'user-001',
    firstName: 'Иван',
    lastName: 'Иванов',
    email: '',
    gender: 'male',
    birthday: '1990-01-01',
    avatar: 'link_to_avatar1.jpg',
  },
  {
    id: 'user-002',
    firstName: 'Мария',
    lastName: 'Петрова',
    email: '',
    gender: 'female',
    birthday: '1988-05-12',
    avatar: 'link_to_avatar2.jpg',
  },
  {
    id: 'user-003',
    firstName: 'Сергей',
    lastName: 'Сидоров',
    email: '',
    gender: 'male',
    birthday: '1985-07-20',
  },
  {
    id: 'user-004',
    firstName: 'Ольга',
    lastName: 'Смирнова',
    email: '',
    gender: 'female',
    birthday: '1992-03-15',
  },
  {
    id: 'user-005',
    firstName: 'Дмитрий',
    lastName: 'Кузнецов',
    email: '',
    gender: 'male',
    birthday: '1989-08-30',
    avatar: 'link_to_avatar3.jpg',
  },
  {
    id: 'user-006',
    firstName: 'Анна',
    lastName: 'Иванова',
    email: '',
    gender: 'female',
    birthday: '1995-12-25',
  },
]

export const mockCertificates: Certificate[] = [
  {
    id: '1',
    title: 'Сертификат безопасности',
    issuedDate: new Date('2023-03-08'),
    issuedUser: mockUsers[0]!,
    image: {
      url: 'https://calendarbox.ru/wp-content/uploads/2019/05/shablon-sertificata-02.jpg',
    },

  },
  {
    id: '2',
    title: 'Сертификат соответствия',
    issuedDate: new Date('2022-12-15'),
    issuedUser: mockUsers[1]!,
  },
  {
    id: '3',
    title: 'Сертификат качества',
    issuedDate: new Date('2021-10-01'),
    issuedUser: mockUsers[2]!,
    image: {
      url: 'https://static.vecteezy.com/system/resources/previews/000/636/160/original/portrait-luxury-certificate-template-with-elegant-border-frame-diploma-design-for-graduation-or-completion-vector.jpg',
    },
  },
  {
    id: '4',
    title: 'Сертификат об образовании',
    issuedDate: new Date('2020-06-30'),
    issuedUser: mockUsers[3]!,
    image: {
      url: 'https://www.mos-elektrika.ru/wp-content/uploads/2016/last_certificate/sert4.jpg',
    },
  },
  {
    id: '5',
    title: 'Сертификат о прохождении курса',
    issuedDate: new Date('2019-04-12'),
    issuedUser: mockUsers[4]!,
  },
]
