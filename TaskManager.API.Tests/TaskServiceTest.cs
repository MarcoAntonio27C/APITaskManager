using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Services;

namespace TaskManager.API.Tests
{
    public class TaskServiceTest
    {
        // Prueba para  verificar que el evento se dispara al completar una tarea

        [Fact]
        public void OnChangeStatus_EventIsTriggered_WhenTaskIsCompleted()
        {

            // Arrange Configurar

            var taskService = new TaskService();

            var taskInitial = taskService.GetById(Guid.Parse("78d02206-5584-4291-b4f3-9478671653a7"));
            Assert.False(taskInitial?.IsCompleted, "La tarea ya esta completada");

            string taskIdCapturado = string.Empty;
            string? nuevoStatusCapturado = null;
            bool eventoDisparado = false;

            taskService.OnChangeStatus += (taskId, newStatus) =>
            {
                taskIdCapturado = taskId.ToString();
                nuevoStatusCapturado = newStatus;
                eventoDisparado = true;
            };

            // Act Ejecutar

             Guid taskIdParaActualizar = Guid.Parse("78d02206-5584-4291-b4f3-9478671653a7");

            // Actualizar el estado de la tarea a completada
            var updatedTask = taskService.UpdateStatus(taskIdParaActualizar, true);


            // Assert Verificar

            Assert.True(eventoDisparado, "El evento OnChangeStatus no se disparó.");
            // ⭐️ Verifica que la expresión booleana sea TRUE.
            Assert.True(taskIdParaActualizar.ToString() == taskIdCapturado.ToString(),"El ID de la tarea capturado por el evento NO coincide con el ID esperado.");
            Assert.True("Completed" == nuevoStatusCapturado, "El estado de la tarea capturado no fue 'Completed'.");
            Assert.True(taskService.GetById(taskIdParaActualizar)?.IsCompleted,"La tarea no se marcó como completada internamente después de llamar a ActualizarStatus.");
            Assert.True("Completed" == nuevoStatusCapturado, "El estado de la tarea capturado por el evento no es 'Completed'.");







        }
    }
}
