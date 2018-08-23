﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CognitiveServices.FaceApi.Models;
using Newtonsoft.Json;

namespace CognitiveServices.FaceApi.Service
{
    public sealed class ApiService
    {
        #region Person Group

        public async Task<IList<Group>> GetPersonGroups()
        {
            try
            {
                var request = await HttpService.GetAsync("persongroups");
                if (request.IsSuccessStatusCode)
                {
                    using (var stream = await request.Content.ReadAsStreamAsync())
                    {
                        var json = await new StreamReader(stream).ReadToEndAsync();
                        return JsonConvert.DeserializeObject<IList<Group>>(json).ToList();
                    }
                }

                await HttpService.ShowError(request);
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
            }
            return new List<Group>();
        }

        public async Task<bool> CreatePersonGroup(Group person)
        {
            try
            {
                var request = await HttpService.PutAsync($"persongroups/{person.PersonGroupId}", person);
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdatePersonGroup(Group person)
        {
            try
            {
                var request = await HttpService.PatchAsync($"persongroups/{person.PersonGroupId}", person);
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        public async Task<bool> DeletePersonGroup(string personGroupId)
        {
            try
            {
                var request = await HttpService.DeleteAsync($"persongroups/{personGroupId}");
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        #endregion

        #region Train

        public void TrainPersonGroup(int personGroupId)
        {
            var url = HttpService.PostAsync($"persongroups/personGroupId/train", new object());
        }

        public void GetPersonGroupTrainingStatus(int personGroupId)
        {
            var url = HttpService.PostAsync($"persongroups/{personGroupId}/training", new  object());
        }

        #endregion

        #region Persons

        public async Task<IList<Person>> GetPersonsByGroup(string personGroupId)
        {
            try
            {
                var request = await HttpService.GetAsync($"persongroups/{personGroupId}/persons");
                if (request.IsSuccessStatusCode)
                {
                    using (var stream = await request.Content.ReadAsStreamAsync())
                    {
                        var json = await new StreamReader(stream).ReadToEndAsync();
                        return JsonConvert.DeserializeObject<IList<Person>>(json).ToList();
                    }
                }

                await HttpService.ShowError(request);
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
            }
            return new List<Person>();
        }

        public void GetPerson(int personGroupId, int personId)
        {
            var url = HttpService.GetAsync($"persongroups/{personGroupId}/persons/{personId}");
        }

        public async Task<bool> CreatePerson(Group group, Person person)
        {
            try
            {
                var request = await HttpService.PostAsync($"persongroups/{group.PersonGroupId}/persons", person);
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                ToastService.Show("Pessoa cadastrada com sucesso!", person.Name);
                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        public async Task<bool> UpdatePerson(Group group, Person person)
        {
            try
            {
                var request = await HttpService.PatchAsync($"persongroups/{group.PersonGroupId}/persons/{person.PersonId}", person);
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                ToastService.Show("Pessoa atualizado com sucesso!", person.Name);
                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        public async Task<bool> DeletePerson(Group group, Person person)
        {
            try
            {
                var request = await HttpService.DeleteAsync($"persongroups/{group.PersonGroupId}/persons/{person.PersonId}");
                if (!request.IsSuccessStatusCode)
                {
                    await HttpService.ShowError(request);
                    return false;
                }

                ToastService.Show("Pessoa excluída com sucesso!", person.Name);
                return true;
            }
            catch (Exception exception)
            {
                await new MessageDialog($"Error Message: {exception.Message}").ShowAsync();
                return false;
            }
        }

        #endregion

        #region Person Face

        public void GetPersonFaces(int personGroupId, int personId)
        {
            var url = HttpService.GetAsync($"persongroups/{personGroupId}/persons/{personId}");
        }

        public void GetPersonFace(int personGroupId, int personId, int faceId)
        {
            var url = HttpService.GetAsync($"persongroups/{personGroupId}/persons/{personId}/persistedfaces/{faceId}");
        }

        public void AddPersonFace(int personGroupId, int personId, string imageUrl)
        {
            var url = HttpService.PostAsync($"persongroups/{personGroupId}/persons/{personId}/persistedfaces?userData={imageUrl}", new object());
        }

        public void DeletePersonFace(int personGroupId, int personId, int faceId)
        {
            var url = HttpService.DeleteAsync($"persongroups/{personGroupId}/persons/{personId}/persistedfaces/{faceId}");
        }

        #endregion

        #region Face List

        public void CreateFaceList(int faceListId)
        {
            var url = HttpService.PutAsync($"facelists/{faceListId}", new object());
        }

        public void AddFace(int faceListId)
        {
            var url = HttpService.PostAsync($"facelists/{faceListId}/persistedFaces", new object());
        }

        #endregion

        #region Face Operations

        public void Detect(string imageUrl)
        {
            var url = HttpService.PostAsync("detect?returnFaceLandmarks=false&returnFaceAttributes=age,gender,smile,glasses,emotion,facialHair", new object());
        }

        public void Identify()
        {
            var url = HttpService.PostAsync("identify", new object());
        }

        public void Group()
        {
            var url = HttpService.PostAsync("group", new object());
        }

        public void FindSimilar()
        {
            var url = HttpService.PostAsync("findsimilars", new object());
        }

        #endregion
    }
}
