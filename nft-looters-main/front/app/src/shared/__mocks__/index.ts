import { CalendarDate } from '@internationalized/date'

import type { User, UserOrganization } from '../model/user'

export const organization: UserOrganization = {
  id: 'org-001',
  name: 'ООО Пример Организации',
  employees: [
    {
      id: 'user-001',
      firstName: 'Иван',
      lastName: 'Иванов',
      gender: 'male',
      birthday: '1990-01-01',
      avatar: 'link_to_avatar1.jpg',
    },
    {
      id: 'user-002',
      firstName: 'Мария',
      lastName: 'Петрова',
      gender: 'female',
      birthday: '1988-05-12',
      avatar: 'link_to_avatar2.jpg',
    },
    {
      id: 'user-003',
      firstName: 'Сергей',
      lastName: 'Сидоров',
      gender: 'male',
      birthday: '1985-07-20',
    },
    {
      id: 'user-004',
      firstName: 'Ольга',
      lastName: 'Смирнова',
      gender: 'female',
      birthday: '1992-03-15',
    },
    {
      id: 'user-005',
      firstName: 'Дмитрий',
      lastName: 'Кузнецов',
      gender: 'male',
      birthday: '1989-08-30',
      avatar: 'link_to_avatar3.jpg',
    },
    {
      id: 'user-006',
      firstName: 'Анна',
      lastName: 'Иванова',
      gender: 'female',
      birthday: '1995-12-25',
    },
    {
      id: 'user-007',
      firstName: 'Алексей',
      lastName: 'Алексеев',
      gender: 'male',
      birthday: '1987-02-14',
      avatar: 'link_to_avatar4.jpg',
    },
    {
      id: 'user-008',
      firstName: 'Екатерина',
      lastName: 'Воронова',
      gender: 'female',
      birthday: '1991-04-05',
    },
    {
      id: 'user-009',
      firstName: 'Николай',
      lastName: 'Тимофеев',
      gender: 'male',
      birthday: '1983-11-17',
    },
    {
      id: 'user-010',
      firstName: 'Юлия',
      lastName: 'Лебедева',
      gender: 'female',
      birthday: '1994-09-03',
      avatar: 'link_to_avatar5.jpg',
    },
  ],
}

export const mockUser: User = {
  id: 'haha',
  avatar: 'https://sun9-48.userapi.com/impg/zPd7y71aGVlopDXhvJ9PA46bIpmsHiB1C0wfrA/1HOPPr6-mOA.jpg?size=1600x900&quality=96&sign=20d8367121e2ff6a41e704b51d378cc3&type=album',
  firstName: 'Матвей',
  lastName: 'Оченьдлиннофамильный',
  gender: 'male',
  birthday: new CalendarDate(2000, 1, 1).toString(),
  organization: {
    id: organization.id,
    name: organization.name,
    employees: [{
      id: 'haha',
      avatar: 'https://sun9-48.userapi.com/impg/zPd7y71aGVlopDXhvJ9PA46bIpmsHiB1C0wfrA/1HOPPr6-mOA.jpg?size=1600x900&quality=96&sign=20d8367121e2ff6a41e704b51d378cc3&type=album',
      firstName: 'Матвей',
      lastName: 'Оченьдлиннофамильный',
      gender: 'male',
      birthday: new CalendarDate(2000, 1, 1).toString(),
    }, ...organization.employees],
  },
}
