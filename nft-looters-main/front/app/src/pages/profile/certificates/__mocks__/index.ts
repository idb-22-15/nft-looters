import type { Certificate, Issuer } from '../types'

export const mockCertificates: Certificate[] = [
  {
    id: '1',
    title: 'Сертификат безопасности',
    issuedDate: new Date('2023-03-08'),
    issuerId: 'ISACA',
    verified: true,
    image: {
      url: 'https://calendarbox.ru/wp-content/uploads/2019/05/shablon-sertificata-02.jpg',
    },
  },
  {
    id: '2',
    title: 'Сертификат соответствия',
    issuedDate: new Date('2022-12-15'),
    issuerId: 'ISO',
    verified: true,
  },
  {
    id: '3',
    title: 'Сертификат качества',
    issuedDate: new Date('2021-10-01'),
    issuerId: 'EFQM',
    verified: false,
    image: {
      url: 'https://static.vecteezy.com/system/resources/previews/000/636/160/original/portrait-luxury-certificate-template-with-elegant-border-frame-diploma-design-for-graduation-or-completion-vector.jpg',
    },
  },
  {
    id: '4',
    title: 'Сертификат об образовании',
    issuedDate: new Date('2020-06-30'),
    issuerId: 'Университет',
    verified: true,
    image: {
      url: 'https://www.mos-elektrika.ru/wp-content/uploads/2016/last_certificate/sert4.jpg',
    },
  },
  {
    id: '5',
    title: 'Сертификат о прохождении курса',
    issuedDate: new Date('2019-04-12'),
    issuerId: 'Платформа онлайн-обучения',
    verified: false,
  },
]

export const mockIssuers: Issuer[] = [
  { id: 'hnhfa36gfd', name: 'Google', certificateTypes: ['Security Certificate', 'Compliance Certificate'] },
  { id: 'ap4vteple', name: 'Apple', certificateTypes: ['Quality Certificate'] },
  { id: 'micsww3rosoft', name: 'Microsoft', certificateTypes: ['Training Certificate', 'Educational Certificate'] },
  { id: 'am24a5zon', name: 'Amazon', certificateTypes: ['Educational Certificate'] },
  { id: 'm2cse762ta', name: 'Meta', certificateTypes: ['Training Certificate'] },
]
