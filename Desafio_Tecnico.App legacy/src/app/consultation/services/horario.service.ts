import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HorarioService {

  constructor() { }

  /**
   * Gera lista de horários no formato HH:mm
   * @param horaInicio Ex: "08:00"
   * @param horaFim Ex: "18:00"
   * @param intervaloMinutos Ex: 30
   */
  gerarHorarios(
    horaInicio: string = '08:00',
    horaFim: string = '18:00',
    intervaloMinutos: number = 30
  ): string[] {

    const horarios: string[] = [];

    let [hora, minuto] = horaInicio.split(':').map(Number);
    const [horaFinal, minutoFinal] = horaFim.split(':').map(Number);

    const data = new Date();
    data.setHours(hora, minuto, 0, 0);

    const dataFinal = new Date();
    dataFinal.setHours(horaFinal, minutoFinal, 0, 0);

    while (data <= dataFinal) {
      horarios.push(this.formatarHora(data));

      data.setMinutes(data.getMinutes() + intervaloMinutos);
    }

    return horarios;
  }

  private formatarHora(data: Date): string {
    const hora = data.getHours().toString().padStart(2, '0');
    const minuto = data.getMinutes().toString().padStart(2, '0');
    return `${hora}:${minuto}`;
  }
}