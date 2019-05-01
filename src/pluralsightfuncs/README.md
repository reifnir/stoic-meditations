Example pipeline
|          Trigger         |            | Output                      |
|-------------------------:|:----------:|-----------------------------|
|Payment Provider Web-hook |-> [FN] -> | Message in Queue             |
| Generate License Message |-> [FN] -> | License File in Blob Storage |
|             License File |-> [FN] -> | Send email to customer       |
|    Report error web-hook |-> [FN] -> | New row in table storage     |
|     Validate license API |-> [FN] -> | Database lookup              |
|   Nightly scheduled task |-> [FN] -> | Generate report              |

## Durable Functions
----------------------------------------
### Orchestrator Constraints
* MUST BE DETERMINISTIC!
* Never use random numbers, DateTime.UtcNow, Guid.NewGuid(), etc
  * Use DurableOrchestrationContext.CurrentUtcDateTime
* Never do I/O directly in the orchestrator (it's single threaded)
  * Do I/O in activity functions
* Don't write infinite loops
  * Use DurableOrchestrationContext.ContinueAsNew()
