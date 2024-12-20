﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PlanningProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jira : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private string JIRA_BASE_URL = "";

        public Jira(HttpClient httpClient)
        {

            var email = Environment.GetEnvironmentVariable("jira_email");
            var api = Environment.GetEnvironmentVariable("jira_api_key");
            JIRA_BASE_URL = Environment.GetEnvironmentVariable("jira_base_url");


            _httpClient = httpClient;
            var byteArray = System.Text.Encoding.ASCII.GetBytes($"{email}:{api}");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            _httpClient.BaseAddress = new Uri(JIRA_BASE_URL);
        }

        [HttpGet("task/{issueKey}/{storyPoints}")]
        public async Task<IActionResult> UpdateStoryPoints(string issueKey, int storyPoints)
        {
            var field = Environment.GetEnvironmentVariable("jira_s_point_field");
            var updateData = new
            {
                fields = new Dictionary<string, int>
                    {
                        { field, storyPoints }
                    }
            };
            var response = await _httpClient.PutAsJsonAsync($"{JIRA_BASE_URL}api/3/issue/{issueKey}", updateData);
            if (response.IsSuccessStatusCode)
            {
                return Ok($"Story points updated for {issueKey} to {storyPoints}");
            }

            return BadRequest("Failed to update story points");
        }  

        [HttpGet("tasks/{customValue}")]
        public async Task<IActionResult> GetTasks(string customValue)
        {
            var response = await _httpClient.GetAsync($"{JIRA_BASE_URL}agile/1.0/sprint/{customValue}/issue");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            return StatusCode((int)response.StatusCode);
        }

        // GET: api/<Jira>/sprints
        [HttpGet("sprints")]
        public async Task<IActionResult> GetSprints()
        {
            var response = await _httpClient.GetAsync($"{JIRA_BASE_URL}agile/1.0/board/3/sprint");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                return Ok(data);
            }
            return StatusCode((int)response.StatusCode);
        }



    }
}
