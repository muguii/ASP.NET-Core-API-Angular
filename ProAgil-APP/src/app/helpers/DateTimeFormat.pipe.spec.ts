import { DateTimeFormatPipe } from './DateTimeFormat.pipe';

describe('DateTimeFormatPipe', () => {
  it('create an instance', () => {
    const pipe = new DateTimeFormatPipe('pt-br');
    expect(pipe).toBeTruthy();
  });
});
